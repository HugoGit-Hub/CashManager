'use client';

import { fetchBanking } from '../utils/FetchBanking';
import { notifications } from '../utils/Notifications';
import { useRouter } from 'next/navigation';

function LoginForm() {
    const router = useRouter();
    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        
        const email = (document.getElementById('email') as HTMLInputElement).value;
        const password = (document.getElementById('password') as HTMLInputElement).value;
        Login({ email, password });
    };
    
    const Login = async (credentials: { email: string; password: string }) => {
        try {
            var request = {
                method: 'POST',
                headers: new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' }),
            };
    
            await fetchBanking(`/Authentication/Login?email=${credentials.email}&password=${credentials.password}`, request)
            .then(response => {
                if (response.ok) {
                    notifications("success", "Connecté");
                    return response.json();
                }            
            
                if (response.status === 401) {
                    notifications("info", "Email ou mot de passe incorrects");
                }
            })
            .then(data => {
                localStorage.setItem("token", data);
                router.push('/account')
            });
        } catch (error) {
            notifications("error", "Une erreur réseau est survenue");
        }
    };

    return (
        <form onSubmit={handleSubmit} className="card-body">
            <div className="form-control">
                <label className="label">
                    <span className="label-text">Email</span>
                </label>

                <input id="email" type="email" placeholder="nom@email.com" className="input input-bordered" required />
            </div>
            <div className="form-control">
                <label className="label">
                    <span className="label-text">Mot de passe</span>
                </label>

                <input id="password" type="password" placeholder="*******" className="input input-bordered" required />

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