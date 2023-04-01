import {
  TransactionScreenState,
  useTransactionScreenContext,
} from "@/contexts/TransactionScreenContext";
import fetchTC from "fetch-with-timeout-and-cache";
import React, { useState, useEffect } from "react";
import EndOfDayBalance from "./EndOfDayBalance";
import { TransactionData } from "./shared/types";
import TransactionForm from "./TransactionForm";
import TransactionTable from "./TransactionTable";

function wait(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

interface EndOfDayBalanceData {
  endOfDayBalance: number;
}

const MainScreen: React.FC = () => {
  const { state, setState } = useTransactionScreenContext();
  const [transactions, setTransactions] = useState<TransactionData[]>([]);
  const [endOfDayBalance, setEndOfDayBalance] = useState<number>();

  useEffect(() => {
    if (
      [
        TransactionScreenState.should_reload_list,
        TransactionScreenState.initial,
      ].includes(state)
    ) {
      fetchTC({
        resource: "http://localhost:5000/transactions",
      })
        .then((response) => (response.ok ? response.json() : Promise.reject.bind(Promise)))
        .then((data: TransactionData[]) => {
          setTransactions(data);

          wait(600).then(() => setState(TransactionScreenState.should_reload_results))
        });
    }
  }, [state]);

  useEffect(() => {
    if (
      [
        TransactionScreenState.should_reload_results,
        TransactionScreenState.initial,
      ].includes(state)
    ) {
      fetchTC({
        resource: "http://localhost:5001/consolidatedresults?date=2023-04-01",
        retryOptions: {
          delay: 2000,
          retries: 2,
        },
      })
        .then((response) => (response.ok ? response.json() : Promise.reject.bind(Promise)))
        .then((data: EndOfDayBalanceData) => {
          setEndOfDayBalance(data.endOfDayBalance);
          setState(TransactionScreenState.loaded);
        });
    }
  }, [state]);

  return (
    <>
      <EndOfDayBalance endOfDayBalance={endOfDayBalance} />
      <TransactionForm />
      <TransactionTable transactionFormDataArray={transactions} />
    </>
  );
};

export default MainScreen;
