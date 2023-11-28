export function provideBank(id: number | undefined) {
    switch (id) {
        case 0:
            return 'CIC';
        case 1:
            return 'LCL';
        case 2:
            return 'Other';
        case 3:
            return 'Groupama';
        case 4:
            return 'BnpParibas';
        case 5:
            return 'CreditMutuel';
        case 6:
            return 'CaisseEpargne';
        case 7:
            return 'BanquePostale';
        case 8:
            return 'CreditAgricole';
        case 9:
            return 'SocieteGenerale';
        case 10:
            return 'BanquePopulaire';
        case 11:
            return 'CreditCooperatif';
    }
}

export function provideBankImage(bankName: string | undefined) {
    switch (bankName) {
        case 'CIC':
            return '/cic.svg';
        case 'LCL':
            return '/lcl.svg';
        case 'Other':
            return '/other.png';
        case 'Groupama':
            return '/groupama.png';
        case 'BnpParibas':
            return '/bnpParibas.jpg';
        case 'CreditMutuel':
            return '/creditMutuel.svg';
        case 'CaisseEpargne':
            return '/caisseEpargne.jpg';
        case 'BanquePostale':
            return '/banquePostale.svg';
        case 'CreditAgricole':
            return '/creditAgricole.svg';
        case 'SocieteGenerale':
            return '/societeGenerale.png';
        case 'BanquePopulaire':
            return '/banquePopulaire.png';
        case 'CreditCooperatif':
            return '/creditCooperatif.jpg';
        default:
            return '/other.png';
    }
}