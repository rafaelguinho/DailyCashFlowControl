import { createContext, ReactNode, useContext, useState } from "react";

export enum TransactionScreenState {
  initial,
  should_reload_list,
  should_reload_results,
  loaded,
}

type TransactionScreenContextType = {
  state: TransactionScreenState;
  setState: (newState: TransactionScreenState) => void;
};

const TransactionScreenContext = createContext<TransactionScreenContextType>({
  state: TransactionScreenState.initial,
  setState: () => {},
});

type TransactionScreenProviderProps = {
    children: ReactNode;
  };
  

export const TransactionScreenProvider: React.FC<TransactionScreenProviderProps> = ({ children }) => {
    const [state, setState] = useState<TransactionScreenState>(
      TransactionScreenState.initial
    );
  
    return (
      <TransactionScreenContext.Provider value={{ state, setState }}>
        {children}
      </TransactionScreenContext.Provider>
    );
  };

export const useTransactionScreenContext = () =>
  useContext(TransactionScreenContext);

export default TransactionScreenContext;