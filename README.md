# CrawfisSoftware.BitManipulation

Bit manipulation helpers for `System.Numerics.BigInteger`.

This package currently exposes two public static classes:

- `BitManipulation.BitUtility` — utility methods for setting/clearing bits, counting set bits, and generating random bit patterns.
- `BitManipulation.BitEnumerators` — enumerators that generate bit patterns with an even/odd number of set bits.

Targets: `.NET Standard 2.1`, `.NET 8`.

## Install

Add the NuGet package:

- Package ID: `CrawfisSoftware.BitManipulation`

## Namespaces

```csharp
using System.Numerics;
using BitManipulation;
```

## Quick start

### Set/clear/check bits

```csharp
BigInteger value = BigInteger.Zero;

// Set bit 5
value = BitUtility.OpenBit(value, 5);

// Check if bit 5 is set (within a width)
bool isSet = BitUtility.IsBitSet(value, pos: 5, width: 16);

// Clear bit 5
value = BitUtility.BlockBit(value, 5);
```

### Create a bit mask for a range

`OpenBits(startingPos, endingPos)` returns a mask with a contiguous run of 1s starting at `startingPos`.

```csharp
// 1s in positions [2..6) => bits 2,3,4,5 set
BigInteger mask = BitUtility.OpenBits(startingPos: 2, endingPos: 6);
```

### Count set bits

```csharp
BigInteger x = 0b101101;
int popCount = BitUtility.CountSetBits(x); // 4
```

### Enumerate patterns with even/odd parity

Generate all bit patterns for a given `width` that have an even (or odd) number of set bits.

```csharp
foreach (BigInteger pattern in BitEnumerators.AllEven(width: 4))
{
    // pattern is a value in [0, 2^width)
}
```

## API overview

### `BitManipulation.BitUtility`

- `bool IsBitSet(BigInteger inflow, int pos, int width)`
  - Checks whether bit `pos` is set, considering only the lowest `width` bits.
- `BigInteger OpenBit(BigInteger bit, int pos)`
  - Sets bit `pos` to 1.
- `BigInteger BlockBit(BigInteger bit, int pos)`
  - Clears bit `pos` to 0.
- `BigInteger OpenBits(int startingPos, int endingPos)`
  - Creates a contiguous mask of 1s starting at `startingPos`.
  - The number of 1s is `endingPos - startingPos`.
- `BigInteger BlockHighestSetBit(BigInteger bit)`
  - Clears the highest set bit.
- `BigInteger BlockLowestSetBit(BigInteger bit)`
  - Clears the lowest set bit.
- `int CountSetBits(BigInteger n)`
  - Counts the number of 1 bits.
- `BigInteger ConcatenateMultipleBits(IList<int> bits, int width)`
  - Concatenates multiple fixed-width bit patterns into one `BigInteger`.
- `List<int> OpeningsFromBits(BigInteger row, int width)`
  - Returns positions of all set bits in the lowest `width` bits.
- `string PrintBits(BigInteger flow, int width)`
  - Returns a 0/1 string representation of the lowest `width` bits.
  - Note: also writes to `Console`.
- `string VisualizeVerticalBits(BigInteger flow, int width)` / `string VisualizeHorizontalBits(BigInteger flow, int width)`
  - Produces simple ASCII visualizations.
  - Note: also writes to `Console`.
- `BigInteger RandomOddBitPattern(int bitLength, Random random)`
  - Returns a random `BigInteger` with an **odd** number of set bits.
- `BigInteger RandomEvenBitPattern(int bitLength, Random random, bool allowZero = false)`
  - Returns a random `BigInteger` with an **even** number of set bits.

### `BitManipulation.BitEnumerators`

- `IEnumerable<BigInteger> AllEven(int width)`
  - Enumerates all values in `[0, 2^width)` with an even number of set bits.
- `IEnumerable<BigInteger> AllOdd(int width)`
  - Enumerates all values in `[0, 2^width)` with an odd number of set bits.

## Notes

- The `AllEven` / `AllOdd` enumerations grow exponentially with `width` (they enumerate ~half of all `2^width` patterns).
- Some visualization helpers write to `Console` as a side-effect.
