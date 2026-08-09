import { useEffect, useState } from "react";

interface FetchProps<T> {
  fetchFunction: () => Promise<T>;
}

function useFetch<T>({ fetchFunction }: FetchProps<T>): {
  data: T | null;
  isLoading: boolean;
  error: Error | null;
  refetch: () => Promise<void>;
} {
  const [response, setResponse] = useState<T | null>(null);

  const [error, setError] = useState<Error | null>(null);
  const [isLoading, setLoading] = useState<boolean>(true);

  const fetchData = async () => {
    try {
      const fetchResult = await fetchFunction();
      setResponse(fetchResult);
    } catch (err) {
      setError(err as Error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchData();
  }, [fetchFunction]);

  return { data: response, isLoading, error, refetch: fetchData };
}

export default useFetch;
