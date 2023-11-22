import React, { useEffect, useState } from 'react';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

export default function useSignalR(): { connection: HubConnection | null; currentConnectionId: string } {
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const [currentConnectionId, setCurrentConnectionId] = useState('');

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
      .withUrl('http://localhost:5000/endOfDayBalanceHub')
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);

    newConnection.start().then(() => {
      console.log('Connected to SignalR hub');
      if(!newConnection.connectionId) return;
      setCurrentConnectionId(newConnection.connectionId);
    }).catch((error) => {
      console.error('Error establishing SignalR connection:', error);
    });

    return () => {
      newConnection.stop();
    };
  }, []);

  return { connection, currentConnectionId };
}