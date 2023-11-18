'use client';

import { useState } from "react";

const LoginForm = () => {
    const credentials = useState<Credentials>({
        email: '',
        password: '',
    });

    const postLogin = async () => {
        const baseUrl = 'https://localhost:7154/api'
        var result = await fetch(baseUrl + '/Login', {
            method: 'POST',
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(credentials),
        });

        if (result.status === 401) {
            console.log('Unauthorized')
            return;
        }
        
        const token = await result.json();
        localStorage.setItem('token', token);
    }

    return (
        <form className="card-body">
            <div className="form-control">
                <label className="label">
                    <span className="label-text">Email</span>
                </label>

                <input type="email" placeholder="nom@email.com" className="input input-bordered" required />
            </div>
            <div className="form-control">
                <label className="label">
                    <span className="label-text">Mot de passe</span>
                </label>

                <input type="password" placeholder="*******" className="input input-bordered" required />

                <label className="label">
                    <a href="#" className="label-text-alt link link-hover">Mot de passe oublié ?</a>
                </label>
            </div>
            <div className="form-control mt-6">
                <button className="btn btn-primary">Connecter</button>
            </div>
        </form>
    )
}

export default LoginForm