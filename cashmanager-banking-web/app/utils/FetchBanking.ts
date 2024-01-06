export function fetchBanking(url: string, options: any) {
    const baseUrl = 'http://g24.epihub.eu:81/api';
    
    return fetch(baseUrl + url, options);
}