export function fetchBanking(url: string, options: any) {
    const baseUrl = 'http://127.0.0.1:5000/api';
    
    return fetch(baseUrl + url, options);
}