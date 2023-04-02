import { TransactionData } from "./shared/types";

type TransactionTableProps = {
  transactionFormDataArray: TransactionData[];
};

const TransactionTable: React.FC<TransactionTableProps> = ({
  transactionFormDataArray,
}) => {
  const formatType = (type: "debit" | "credit") => {
    if (type === "debit") return "Débito";
    if (type === "credit") return "Crédito";

    return "-";
  };
  return (
    <div className="max-w-full overflow-x-auto dark:bg-gray-800">
      <table className="min-w-full divide-y divide-gray-200 dark:divide-gray-600">
        <thead className="bg-gray-50 dark:bg-gray-700">
          <tr>
            <th
              scope="col"
              className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider dark:text-gray-300"
            >
              Tipo
            </th>
            <th
              scope="col"
              className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider dark:text-gray-300"
            >
              Valor
            </th>
            <th
              scope="col"
              className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider dark:text-gray-300"
            >
              Descrição
            </th>
            <th
              scope="col"
              className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider  dark:text-gray-300"
            >
              Data
            </th>
          </tr>
        </thead>
        <tbody className="bg-white divide-y divide-gray-200 dark:bg-gray-900 dark:divide-gray-600">
          {transactionFormDataArray.map((formData) => (
            <tr key={formData.type}>
              <td className="px-6 py-4 whitespace-nowrap">
                <div className="text-sm text-gray-900 dark:text-gray-300">
                  {formatType(formData.type)}
                </div>
              </td>
              <td className="px-6 py-4 whitespace-nowrap">
                <div className="text-sm text-gray-900 dark:text-gray-300">
                  {new Intl.NumberFormat(undefined, {
                    style: "currency",
                    currency: "BRL",
                  }).format(formData.value)}
                </div>
              </td>
              <td className="px-6 py-4 whitespace-nowrap">
                <div className="text-sm text-gray-900 dark:text-gray-300">
                  {formData.description}
                </div>
              </td>
              <td className="px-6 py-4 whitespace-nowrap">
                <div className="text-sm  dark:text-gray-300">
                  {new Intl.DateTimeFormat("pt-BR").format(
                    new Date(formData.date)
                  )}
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default TransactionTable;
