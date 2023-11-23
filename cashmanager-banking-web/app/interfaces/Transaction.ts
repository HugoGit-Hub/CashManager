export interface Transaction {
    id: number;
    creditor: string;
    debtor: string;
    type: number;
    amount: number;
    state: number;
    date: string;
    guid: string;
}