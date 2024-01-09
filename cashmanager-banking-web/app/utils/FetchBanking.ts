export function fetchBanking(url: string, options: any) {
    const baseUrl = 'https://g24.epihub.eu:445/api';
    
    return fetch(baseUrl + url, options);
}