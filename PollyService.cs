using Polly;
using Polly.CircuitBreaker;
using Polly.Fallback;
using Polly.Retry;
using Polly.Timeout;
using System;

namespace _5_Amazing_Libraries
{
    public class PollyService
    {
        private readonly Random _random = new();

        public void RetryPolicyExample()
        {
            Console.WriteLine("\n--- RetryPolicyExample ---");

            var retryPolicy = Policy
                .Handle<Exception>()
                .Retry(3, (ex, attempt) =>
                {
                    Console.WriteLine($"Retry {attempt}: {ex.Message}");
                });

            retryPolicy.Execute(() =>
            {
                if (_random.Next(1, 4) != 3)
                    throw new Exception("Transient failure!");
                Console.WriteLine("Operation succeeded.");
            });
        }

        public void CircuitBreakerExample()
        {
            Console.WriteLine("\n--- CircuitBreakerExample ---");

            var circuitPolicy = Policy
                .Handle<Exception>()
                .CircuitBreaker(2, TimeSpan.FromSeconds(5),
                    onBreak: (ex, ts) => Console.WriteLine($"Circuit broken: {ex.Message}"),
                    onReset: () => Console.WriteLine("Circuit reset"),
                    onHalfOpen: () => Console.WriteLine("Circuit is half-open")
                );

            for (int i = 1; i <= 5; i++)
            {
                try
                {
                    circuitPolicy.Execute(() =>
                    {
                        Console.WriteLine($"Attempt {i}");
                        throw new Exception("Simulated failure");
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Handled: {ex.Message}");
                }

                Thread.Sleep(1000);
            }
        }

        public void WrapPoliciesExample()
        {
            Console.WriteLine("\n--- WrapPoliciesExample (Retry + Fallback + Timeout) ---");

            var timeoutPolicy = Policy.Timeout(1, TimeoutStrategy.Pessimistic, onTimeout: (ctx, ts, task) =>
            {
                Console.WriteLine("Timeout occurred!");
            });

            var fallbackPolicy = Policy
                .Handle<Exception>()
                .Fallback(() =>
                {
                    Console.WriteLine("Fallback executed. Returning default.");
                });

            var retryPolicy = Policy
                .Handle<Exception>()
                .Retry(2, (ex, attempt) =>
                {
                    Console.WriteLine($"Retry {attempt}: {ex.Message}");
                });

            var wrap = fallbackPolicy.Wrap(retryPolicy).Wrap(timeoutPolicy);

            wrap.Execute(() =>
            {
                Console.WriteLine("Starting risky operation...");
                Thread.Sleep(2000); // Simulate long task
                Console.WriteLine("Completed risky operation.");
            });
        }
    }

}
