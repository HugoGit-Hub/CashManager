export function fetchBanking(url: string, options: any) {
    const baseUrl = 'http://localhost:5000/api';
    
    return fetch(baseUrl + url, options);
}