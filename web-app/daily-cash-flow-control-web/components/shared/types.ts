export type TransactionDataBase = {
  type: "debit" | "credit";
  value: number;
  description: string;
};

export type TransactionData = TransactionDataBase & {
  date: string;
};

export type TransactionFormData = TransactionDataBase & {};
