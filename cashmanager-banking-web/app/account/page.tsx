'use client';

import React, { useEffect, useState } from 'react'
import NavigationBar from '../components/NavigationBar'
import { useRouter } from 'next/navigation';
import { notifications } from '../utils/Notifications';
import { fetchBanking } from '../utils/FetchBanking';

const Account = () => {
  const router = useRouter();
  const [accounts, setAccounts] = useState<Account[]>([]);

  const handleRowClick = () => {
    router.push('/transaction');
  };
  
  useEffect(() => {
    getAccounts();
  }, []);
  
  const getAccounts = async () => {
    try {
      var request = {
        method: "GET",
        headers: new Headers({
          "Content-Type": "application/json",
          "Authorization": `Bearer ${localStorage.getItem("token")}`,
        }),
      }

      await fetchBanking(`/Account/Get`, request)
      .then(response => {
        if (response.ok) {
          notifications("success", "Compte(s) récupéré(s)");
          return response.json();
        }            
    
        if (response.status === 401) {
          notifications("info", "Sesssion expirée");
          router.push("/");
        }
      })
      .then(data => {
        setAccounts(data);
        console.log(data);
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
                <tr key={account.Id} className="hover" onClick={handleRowClick}>
                  <th>{account.Id}</th>
                  <td>{account.Nummber}</td>
                  <td>{account.Owner}</td>
                  <td>{account.Value} €</td>
                </tr>
              ))
            ) : (
              <tr>
                <td>Aucun comptes bancaires</td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  )
}

export default Account