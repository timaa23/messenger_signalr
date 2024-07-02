export function invariant(
  value: unknown,
  message = "Invariant violation"
): asserts value {
  if (value) {
    return;
  }

  throw new Error(message);
}
