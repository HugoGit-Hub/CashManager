export function fetchBanking(url: string, options: any) {
    const baseUrl = 'https://g24.epihub.eu/api';
    
    return fetch(baseUrl + url, options);
}