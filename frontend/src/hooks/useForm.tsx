import { useState } from "react";

interface FormProps<Params> {
  formValidation?: (params: Params) => void;
  formSubmit: (params: Params) => Promise<void>;
  onSuccess?: () => void;
}

function useForm<Params>({
  formValidation,
  formSubmit,
  onSuccess,
}: FormProps<Params>): {
  submitForm: (params: Params) => Promise<void>;
  error: Error | null;
  isSubmitting: boolean;
} {
  const [error, setError] = useState<Error | null>(null);
  const [isSubmitting, setSubmitting] = useState(false);

  const submitForm = async (params: Params) => {
    if (isSubmitting) {
      return;
    }

    setError(null);
    setSubmitting(true);

    try {
      formValidation?.(params);

      await formSubmit(params);

      onSuccess?.();
    } catch (err) {
      setError(err as Error);
    } finally {
      setSubmitting(false);
    }
  };

  return { submitForm, error, isSubmitting };
}

export default useForm;
