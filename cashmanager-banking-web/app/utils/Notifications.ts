// @ts-ignore
import {NotificationManager} from 'react-notifications';

export function notifications(type: string, message: string) {
    switch (type) {
        case 'info':
            NotificationManager.info(message, 'Info', 1500);
            break;
        case 'success':
            NotificationManager.success(message, 'Success', 1500);
            break;
        case 'warning':
                NotificationManager.warning(message, 'Warnning', 1500);
                break;
        case 'error':
            NotificationManager.error(message, 'Error', 1500);
            break;
    }
};