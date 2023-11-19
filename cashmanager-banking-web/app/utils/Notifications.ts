// @ts-ignore
import {NotificationManager} from 'react-notifications';

export function notifications(type: string, message: string) {
    switch (type) {
        case 'info':
            NotificationManager.info(message, 'Info', 3000);
            break;
        case 'success':
            NotificationManager.success(message, 'Success', 3000);
            break;
        case 'warning':
                NotificationManager.warning(message, 'Warnning', 3000);
                break;
        case 'error':
            NotificationManager.error(message, 'Error', 3000);
            break;
    }
};