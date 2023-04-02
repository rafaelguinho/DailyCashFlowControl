import React from "react";
import { useForm, SubmitHandler } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { TransactionFormData } from "./shared/types";
import {
  TransactionScreenState,
  useTransactionScreenContext,
} from "@/contexts/TransactionScreenContext";

const schema = yup.object().shape({
  type: yup.string().oneOf(["credit", "debit"]).required(),
  value: yup.number().positive().required(),
  description: yup.string().required(),
});

export default function TransactionForm() {
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<TransactionFormData>({
    resolver: yupResolver(schema),
  });

  const { setState } = useTransactionScreenContext();

  const onSubmit: SubmitHandler<TransactionFormData> = async (
    data: TransactionFormData
  ) => {
    try {
      await fetch("http://localhost:5000/transactions", {
        method: "POST", // or 'PUT'
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
      });

      setState(TransactionScreenState.should_reload_list);
      reset();
    } catch (error) {}
  };

  return (
    <form
      onSubmit={handleSubmit(onSubmit)}
      className="h-50 flex flex-col md:flex-row md:items-center bg-black rounded-lg p-8"
    >
      <div className="flex-grow mb-4 md:mr-4">
        <label htmlFor="type" className="block text-white font-bold mb-2">
          Type:
        </label>
        <div className="flex items-center">
          <input
            type="radio"
            value="credit"
            {...register("type")}
            id="credit"
            className="form-radio h-5 w-5 text-blue-500"
          />
          <label htmlFor="credit" className="ml-2 font-normal text-white">
            Credito
          </label>
        </div>
        <div className="flex items-center">
          <input
            type="radio"
            value="debit"
            {...register("type")}
            id="debit"
            className="form-radio h-5 w-5 text-blue-500"
          />
          <label htmlFor="debit" className="ml-2 font-normal text-white">
            Débito
          </label>
        </div>
        {errors.type && (
          <span className="text-red-500">{errors.type.message}</span>
        )}
      </div>
      <div className="flex-grow mb-6 w-56">
        <label htmlFor="value" className="block text-white font-bold mb-2">
          Value:
        </label>
        <input
          type="number"
          {...register("value")}
          id="value"
          className="shadow appearance-none border rounded w-full py-2 px-3 text-white leading-tight focus:outline-none focus:shadow-outline"
        />
        {errors.value && (
          <span className="text-red-500">{errors.value.message}</span>
        )}
      </div>
      <div className="flex-grow mb-6 ml-6 w-auto">
        <label
          htmlFor="description"
          className="block text-white font-bold mb-2"
        >
          Description:
        </label>
        <input
          type="text"
          {...register("description")}
          id="description"
          className="shadow appearance-none border rounded w-full py-2 px-3 text-white leading-tight focus:outline-none focus:shadow-outline"
        />
        {errors.description && (
          <span className="text-red-500">{errors.description.message}</span>
        )}
      </div>
      <div>
        <button
          type="submit"
          className="mt-1.5 ml-6 bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
        >
          Salvar
        </button>
      </div>
    </form>
  );
}
