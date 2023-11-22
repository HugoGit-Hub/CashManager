'use client';

import React, { useEffect, useState } from 'react'
import NavigationBar from '../components/NavigationBar'
import { useRouter } from 'next/navigation';
import { notifications } from '../utils/Notifications';
import { fetchBanking } from '../utils/FetchBanking';
import { Account } from '../interfaces/Account';

const Account = () => {
  const router = useRouter();
  const [accounts, setAccounts] = useState<Account[]>([]);

  const handleRowClick = (accountNumber: string) => {
    router.push(`/transaction?accountNumber=${accountNumber}`);
  };
  
  useEffect(() => {
    getAccounts();
  }, []);
  
  const getAccounts = () => {
    try {
      var request = {
        method: "GET",
        headers: new Headers({
          "Content-Type": "application/json",
          "Authorization": `Bearer ${localStorage.getItem("token")}`,
        }),
      }

      fetchBanking(`/Account/Get`, request)
      .then(async response => {
        if (response.ok) {
          notifications("success", "Compte(s) récupéré(s)");
          const data = response.json();
          setAccounts(await data);
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

  return (
    <div>
      <NavigationBar />
      <div className="overflow-x-auto">
        <table className="table">
          <thead>
            <tr >
              <th>Id</th>
              <th>Numéro de compte</th>
              <th>Nom</th>
              <th>Montant</th>
            </tr>
          </thead>
          <tbody>
            {accounts.length > 0 ? (
              accounts.map(account => (
                <tr key={account.id} className="hover" onClick={() => handleRowClick(account.number)}>
                  <th>{account.id}</th>
                  <td>{account.number}</td>
                  <td>{account.owner}</td>
                  <td>{account.value} €</td>
                </tr>
              ))
            ) : (
              <tr>
                <td>Aucun compte bancaire</td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  )
}

export default Account