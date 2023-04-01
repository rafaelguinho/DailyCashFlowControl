type EndOfDayBalanceProps = {
  endOfDayBalance: number | undefined;
};

const EndOfDayBalance: React.FC<EndOfDayBalanceProps> = ({
  endOfDayBalance,
}) => {
  return (
    <div className="bg-gray-900 text-white rounded-md p-4 flex items-center shadow-lg">
      <div>
        <p className="text-xl font-semibold">Resultado consolidado do dia</p>
        <p className="text-4xl font-bold">
          {endOfDayBalance
            ? new Intl.NumberFormat(undefined, {
                style: "currency",
                currency: "BRL",
              }).format(endOfDayBalance)
            : "-"}
        </p>
      </div>
    </div>
  );
};

export default EndOfDayBalance;
