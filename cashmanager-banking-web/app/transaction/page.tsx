'use client';

import React, { useEffect, useState } from 'react'
import NavigationBar from '../components/NavigationBar'
import { Transaction } from '../interfaces/Transaction';
import { notifications } from '../utils/Notifications';
import { fetchBanking } from '../utils/FetchBanking';
import { useRouter, useSearchParams } from 'next/navigation';

function TransactionPage(): React.JSX.Element {
  const router = useRouter();
  const searchParams = useSearchParams();
  var [transactions, setTransactions] = useState<Transaction[]>([]);
  var reversedTransactions = [...transactions].reverse();

  const getTransactions = React.useCallback((number: string | null) => {
    try {
      var request = {
        method: "GET",
        headers: new Headers({
          "Content-Type": "application/json",
          "Authorization": `Bearer ${localStorage.getItem("token")}`,
        }),
      };
      
      fetchBanking(`/Transaction/GetByUserAccounts?accountNumber=${number}`, request)
      .then(async (response) => {
        if (response.ok) {
          notifications("success", "Transaction(s) récupérée(s)");
          const data = response.json();
          setTransactions(await data);
        }
        
        if (response.status === 401) {
          router.push("/");
        }
      });
    } catch (error) {
      notifications("error", "Une erreur réseau est survenue");
    }
  }, [setTransactions, router]);
  
  useEffect(() => {
    getTransactions(searchParams.get('accountNumber'));
  }, [getTransactions, searchParams]);

  const getTransactionState = (state: number) => {
    switch (state) {
      case 0:
        return 'text-success';
      case 1:
        return 'text-warning';
      case 2:
        return 'text-error';
      default:
        return '';
    }
  };

  const getTransactionIdArray = (id: number) => {
    return id % 2 === 0 ? 'timeline-end' : 'timeline-start';
  }

  const validate = async (transaction: Transaction) => {
    try {
      var request = {
        method: "PUT",
        headers: new Headers({
          "Content-Type": "application/json",
          "Authorization": `Bearer ${localStorage.getItem("token")}`,
        }),
        body: JSON.stringify(transaction),
      };

      await fetchBanking(`/Transaction/ValidateTransaction`, request)
      .then(async (response) => {
        if (response.ok) {
          notifications("success", "Transaction validée");
          getTransactions(searchParams.get('accountNumber'));
        }

        if (response.status === 400) {
          notifications("error", "La transaction n'as pas pu aboutir");
          getTransactions(searchParams.get('accountNumber'));
        }
        
        if (response.status === 401) {
          notifications("info", "Session expirée");
          router.push("/");
        }
      });
    } catch (error) {
      notifications("error", "Une erreur réseau est survenue");
    }
  }

  const abort = async (transaction: Transaction) => {
    try {
      var request = {
        method: "PUT",
        headers: new Headers({
          "Content-Type": "application/json",
          "Authorization": `Bearer ${localStorage.getItem("token")}`,
        }),
        body: JSON.stringify(transaction),
      };

      await fetchBanking(`/Transaction/AbortTransaction`, request)
      .then(async (response) => {
        if (response.ok) {
          notifications("success", "Transaction avortée");
          getTransactions(searchParams.get('accountNumber'));
        }

        if (response.status === 400) {
          notifications("error", "La transaction n'as pas pu être avortée");
          getTransactions(searchParams.get('accountNumber'));
        }
        
        if (response.status === 401) {
          notifications("info", "Session expirée");
          router.push("/");
        }
      });
    } catch (error) {
      notifications("error", "Une erreur réseau est survenue");
    }
  }

  return (
    <div>
      <NavigationBar />
      <p className='text-center text-xl'>
        <strong>Compte bancaire : {searchParams.get('accountNumber')}</strong>
      </p>
      <ul className="timeline timeline-vertical">
        {reversedTransactions.map((transaction, index) => (
          transaction.state === 1 && transaction.creditor === searchParams.get('accountNumber') ? ( 
          <li key={index}>
            <hr />
            <div className={`${getTransactionIdArray(index)} timeline-box`}>
              <p><strong>Transaction :</strong> {transaction.guid}</p>
              <p><strong>Envoyé à :</strong> {transaction.debtor}</p>
              <p><strong>Le :</strong> {transaction.date.split('T')[0]}</p>
              <div className='flex items-center'>
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="#dc2626" className="w-5 h-5">
                  <path fillRule="evenodd" d="M10 3a.75.75 0 01.75.75v10.638l3.96-4.158a.75.75 0 111.08 1.04l-5.25 5.5a.75.75 0 01-1.08 0l-5.25-5.5a.75.75 0 111.08-1.04l3.96 4.158V3.75A.75.75 0 0110 3z" clipRule="evenodd" />
                </svg>
                <p className='text-red-600'><strong>- {transaction.amount} €</strong></p>
              </div>
              <div>
                <button onClick={() => validate(transaction)} className='btn btn-success'>Valider</button>
                <button onClick={() => abort(transaction)} className='btn btn-error mx-3'>Annuler</button>
              </div>
            </div>
            <div className="timeline-middle">
              <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" className={`w-5 h-5 ${getTransactionState(transaction.state)}`}>
                <path fillRule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.857-9.809a.75.75 0 00-1.214-.882l-3.483 4.79-1.88-1.88a.75.75 0 10-1.06 1.061l2.5 2.5a.75.75 0 001.137-.089l4-5.5z" clipRule="evenodd" />
              </svg>
            </div>
            <hr />
          </li>
        ) : (
          <li key={index}>
            <hr />
            <div className={`${getTransactionIdArray(index)} timeline-box`}>
              <p><strong>Transaction :</strong> {transaction.guid}</p>
              <p><strong>Envoyé à :</strong> {transaction.debtor}</p>
              <p><strong>Le :</strong> {transaction.date.split('T')[0]}</p>
              {transaction.creditor === searchParams.get('accountNumber') ? (
                  <div className='flex items-center'>
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="#dc2626" className="w-5 h-5">
                      <path fillRule="evenodd" d="M10 3a.75.75 0 01.75.75v10.638l3.96-4.158a.75.75 0 111.08 1.04l-5.25 5.5a.75.75 0 01-1.08 0l-5.25-5.5a.75.75 0 111.08-1.04l3.96 4.158V3.75A.75.75 0 0110 3z" clipRule="evenodd" />
                    </svg>
                    <p className='text-red-600'><strong>- {transaction.amount} €</strong></p>
                  </div>
                ) : (
                  <div className='flex items-center'>
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="#65a30d" className="w-5 h-5">
                      <path fillRule="evenodd" d="M10 17a.75.75 0 01-.75-.75V5.612L5.29 9.77a.75.75 0 01-1.08-1.04l5.25-5.5a.75.75 0 011.08 0l5.25 5.5a.75.75 0 11-1.08 1.04l-3.96-4.158V16.25A.75.75 0 0110 17z" clipRule="evenodd" />
                    </svg>
                    <p className='text-green-600'><strong>+ {transaction.amount} €</strong></p>
                  </div>
                )
              }
            </div>
            <div className="timeline-middle">
              <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" className={`w-5 h-5 ${getTransactionState(transaction.state)}`}>
                <path fillRule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.857-9.809a.75.75 0 00-1.214-.882l-3.483 4.79-1.88-1.88a.75.75 0 10-1.06 1.061l2.5 2.5a.75.75 0 001.137-.089l4-5.5z" clipRule="evenodd" />
              </svg>
            </div>
            <hr />
          </li>
        )))}
        <li>
          <hr />
          <div className="timeline-start timeline-box">Compte créé</div>
          <div className="timeline-middle">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" className="w-5 h-5 text-info">
              <path fillRule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.857-9.809a.75.75 0 00-1.214-.882l-3.483 4.79-1.88-1.88a.75.75 0 10-1.06 1.061l2.5 2.5a.75.75 0 001.137-.089l4-5.5z" clipRule="evenodd" />
            </svg>
          </div>
        </li>
      </ul>
    </div>
  );
}

export default TransactionPage