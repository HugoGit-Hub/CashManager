'use client';

import React, { useEffect, useState } from 'react'
import NavigationBar from '../components/NavigationBar'
import { Transaction } from '../interfaces/Transaction';
import { notifications } from '../utils/Notifications';
import { fetchBanking } from '../utils/FetchBanking';
import { useRouter } from 'next/navigation';

function Transaction(accountNumber: string) {
  const router = useRouter();
  const [transactions, setTransactions] = useState<Transaction[]>([]);

  useEffect(() => {
    getTransactions(accountNumber);
  }, []);

  const getTransactions = (number: string) => {
    try {
      var request = {
        method: "GET",
        headers: new Headers({
          "Content-Type": "application/json",
          "Authorization": `Bearer ${localStorage.getItem("token")}`,
        }),
      };

      fetchBanking(`/Transaction/GetByUserAccounts?${number}`, request)
        .then(async (response) => {
          if (response.ok) {
            notifications("success", "Transaction(s) récupérée(s)");
            const data = response.json();
            setTransactions(await data);
          }

          if (response.status === 401) {
            notifications("info", "Sesssion expirée");
            router.push("/");
          }
        });
    } catch (error) {
      notifications("error", "Une erreur réseau est survenue");
    }
  };

  console.log(transactions);

  return (
    <div>
      <NavigationBar />
      <ul className="timeline timeline-vertical">
        <li>
          <div className="timeline-middle">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" className="w-5 h-5 text-success">
              <path fillRule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.857-9.809a.75.75 0 00-1.214-.882l-3.483 4.79-1.88-1.88a.75.75 0 10-1.06 1.061l2.5 2.5a.75.75 0 001.137-.089l4-5.5z" clipRule="evenodd" />
            </svg>
          </div>
          <div className="timeline-end timeline-box">iMac</div>
          <hr />
        </li>

        <li>
          <hr />
          <div className="timeline-start timeline-box">iPod</div>
          <div className="timeline-middle">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" className="w-5 h-5 text-error">
              <path fillRule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.857-9.809a.75.75 0 00-1.214-.882l-3.483 4.79-1.88-1.88a.75.75 0 10-1.06 1.061l2.5 2.5a.75.75 0 001.137-.089l4-5.5z" clipRule="evenodd" />
            </svg>
          </div>
          <hr />
        </li>

        <li>
          <hr />
          <div className="timeline-middle">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" className="w-5 h-5 text-warning">
              <path fillRule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.857-9.809a.75.75 0 00-1.214-.882l-3.483 4.79-1.88-1.88a.75.75 0 10-1.06 1.061l2.5 2.5a.75.75 0 001.137-.089l4-5.5z" clipRule="evenodd" />
            </svg>
          </div>
          <div className="timeline-end timeline-box">iPhone</div>
          <hr />
        </li>

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

export default Transaction