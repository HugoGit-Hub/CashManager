import React, { useEffect, useState } from 'react'
import { useRouter } from "next/navigation";
import Link from "next/link"
import Image from "next/image";

import { notifications } from "../utils/Notifications";
import { fetchBanking } from "../utils/FetchBanking";
import { Transaction } from "../interfaces/Transaction";
import { User } from "../interfaces/User";
import { provideBank, provideBankImage } from "../utils/UserService";

function NavigationBar() {
    const router = useRouter();
    const [transactions, setTransactions] = useState<Transaction[]>([]);
    const [currentUser, setCurrentUser] = useState<User>();
    const [bank, setBank] = useState<string>();

    const getCurrentUser = React.useCallback(() => {
        try {
            var request = {
                method: "GET",
                headers: new Headers({
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${localStorage.getItem("token")}`,
                }),
            };

            fetchBanking(`/User/GetCurrentUser`, request)
            .then(async (response) => {
                if (response.ok) {
                    const data = response.json();
                    setCurrentUser(await data);
                }
                    
                if (response.status === 401) {
                    notifications("info", "Session expirée");
                    router.push("/");
                }
            });
        } catch (error) {
            notifications("error", "Une erreur réseau est survenue");
        }
    }, [setCurrentUser, router]);

    const getPendingTransactionsForUser = React.useCallback(() => {
        try {
            var request = {
                method: "GET",
                headers: new Headers({
                "Content-Type": "application/json",
                "Authorization": `Bearer ${localStorage.getItem("token")}`,
                }),
            };

            fetchBanking(`/Transaction/GetPendingTransactionsForUser`, request)
            .then(async (response) => {
                if (response.ok) {
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
        getPendingTransactionsForUser();
        getCurrentUser();
        setBank(provideBank(currentUser?.bank));
    }, [getPendingTransactionsForUser, getCurrentUser, currentUser?.bank, setBank]);

    const isTransactionArrayEmpty = () => {
        return transactions.length === 0 ? '' : 'badge badge-xs badge-primary';
    }

    return (
        <div className={`navbar bg-base-100`}>
            <div className="navbar-start">
                <div className="dropdown dropdown-bottom">
                    <label tabIndex={0} className="btn btn-ghost btn-circle">
                        <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M4 6h16M4 12h16M4 18h7" />
                        </svg>
                    </label>
                    <ul tabIndex={0} className="menu menu-sm dropdown-content mt-3 z-[1] p-2 shadow bg-base-100 rounded-box w-52">
                        <Link className="m-2" href={"/account"}>Mes comptes</Link>
                        <Link className="text-error m-2" href={"/"}>Déconnexion</Link>
                    </ul>
                </div>
            </div>
            <div className="navbar-center">
                <Link href={"/account"}>
                    <Image width={10} height={10} className="w-10 h-10" src={provideBankImage(bank)} alt="" />
                </Link>
            </div>
            <div className="navbar-end">
                <button className="btn btn-ghost btn-circle">
                    <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
                    </svg>
                </button>
                <div className="dropdown dropdown-bottom dropdown-end">
                    <label tabIndex={0} className="btn btn-ghost btn-circle">
                        <div className="indicator">
                            <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9" />
                            </svg>
                            <span className={`${isTransactionArrayEmpty()} indicator-item`}></span>
                        </div>
                    </label>
                    <ul tabIndex={0} className="menu menu-sm dropdown-content mt-3 z-[1] p-2 shadow bg-base-100 rounded-box w-52">
                        <p><strong>Transaction(s) en attente(s) de validation</strong></p>
                        {transactions.map(transaction => (
                            <Link key={transaction.id} className="m-2" href={`/transaction?accountNumber=${transaction.creditor}`}>Transaction du : {transaction.date.split('T')[0]}</Link>    
                        ))}
                    </ul>
                </div>
            </div>
        </div>
    )
}

export default NavigationBar