'use client';

import './globals.css'
import 'react-notifications/lib/notifications.css';

import React from "react";
import {NotificationContainer} from 'react-notifications';

export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="fr" data-theme="light">
      <body>
        {children}
        <NotificationContainer />
      </body>
    </html>
  )
}
