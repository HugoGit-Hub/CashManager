'use client';

import React from 'react'
import NavigationBar from '../components/NavigationBar'
import { useRouter } from 'next/navigation';
import { notifications } from '../utils/Notifications';

const Account = () => {
  const router = useRouter();
  let accounts = [];

  const handleRowClick = () => {
    router.push('/transaction');
  };

  const getAccounts = async () => {
    try {
      var request = {
        method: "GET",
        headers: new Headers({
          "Content-Type": "application/json",
          "Authorization": `Bearer ${localStorage.getItem("token")}`,
        }),
      }

      await fetch(`${process.env.NEXT_PUBLIC_BASE_URL}/Account/Get`, request)
      .then(response => {
        if (response.ok) {
          notifications("success", "Compte(s) récupéré(s)");
          return response.json();
        }            
    
        if (response.status === 401) {
          notifications("info", "Email ou mot de passe incorrects");
        }
      })
      .then(data => {
        accounts = data;
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
            <tr>
              <th>Id</th>
              <th>Numéro de compte</th>
              <th>Nom</th>
              <th>Montant</th>
            </tr>
          </thead>
          <tbody>
            <tr className="hover" onClick={handleRowClick}>
              <th>1</th>
              <td>7162534129374650980325648293046354</td>
              <td>User</td>
              <td>300 $</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  )
}

export default Account