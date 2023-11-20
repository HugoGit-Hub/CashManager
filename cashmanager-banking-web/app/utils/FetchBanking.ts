export function fetchBanking(url: string, options: any) {
    const baseUrl = 'https://localhost:7154/api';
    
    return fetch(baseUrl + url, options);
}