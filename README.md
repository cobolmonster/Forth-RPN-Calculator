# RPN Calculator in Forth

A scientific calculator using Reverse Polish Notation (RPN), written in [Forth](https://en.wikipedia.org/wiki/Forth_(programming_language)) and run with [gforth](https://gforth.org/). It supports basic operations, scientific functions, a memory register, and lets you define your own functions on the fly — these are automatically saved and reloaded on every launch.

No compilation required: the script runs directly through the gforth interpreter.

## Installing gforth

### Linux (Debian / Ubuntu)

```bash
sudo apt update
sudo apt install gforth
```

gforth is packaged in most distributions. On Fedora: `sudo dnf install gforth`. On Arch: `sudo pacman -S gforth`.

### Windows

gforth doesn't have a recent Windows installer, so we use the latest official one available (version 0.7.0, still fully functional):

1. Download the official installer: https://www.complang.tuwien.ac.at/forth/gforth/gforth-0.7.0.exe
2. Run it and complete the installation (next-next-finish).
3. The program isn't necessarily added to PATH. To find it:
   - type "gforth" in the Start menu search;
   - if a result shows up, right-click it → **Open file location** to see the actual install folder;
   - otherwise, manually look inside `C:\Program Files (x86)\` for a `gforth` folder.
4. In that folder, `gforth.exe` sits directly at the root (no `bin` subfolder). Check with:
   ```powershell
   dir *.exe
   ```
5. Test it:
   ```powershell
   .\gforth.exe --version
   ```

To avoid typing the full path every time, add this folder to PATH: Settings → System → About → Advanced system settings → Environment Variables → `Path` variable → Edit → New → paste the path to the folder containing `gforth.exe`. Then restart your terminal.

## Running the script

### Linux / macOS

```bash
gforth rpn_calculator.fs
```

or, as an executable:

```bash
chmod +x rpn_calculator.fs
./rpn_calculator.fs
```

### Windows

If `gforth` was added to PATH:

```powershell
gforth rpn_calculator.fs
```

Otherwise, using the full path to the executable:

```powershell
& "C:\Program Files (x86)\gforth\gforth.exe" rpn_calculator.fs
```

In both cases, `cd` into the folder containing `rpn_calculator.fs` first.

## Using the calculator

Once launched, the calculator displays the current stack state and a `rpn>` prompt.

### Reverse Polish Notation

You type the numbers first, then the operator. No parentheses, no operator precedence to worry about: everything reads left to right.

Floating-point numbers must use Forth's scientific notation, i.e. suffixed with `e0` (e.g. `3.0e0`, not `3` or `3.0`).

### Examples

```
3.0e0 4.0e0 +                      -> 7.0
5.0e0 sqrt                         -> square root of 5
2.0e0 3.0e0 ^                      -> 2 to the power of 3 = 8.0
pi 2.0e0 *                         -> 2 * pi
4.0e0 carre 5.0e0 carre + sqrt     -> hypotenuse of a 4-5 triangle (if "carre" is defined)
```

Results stay on the stack and can be reused in subsequent calculations.

### Available operators and functions

| Category    | Available words |
|-------------|-------------------|
| Operators   | `+` `-` `*` `/` `^` (power) |
| Functions   | `sqrt` `sin` `cos` `tan` `log` (base 10) `ln` (natural) |
| Constants   | `pi` `e` |
| Memory      | `ms` (store) `mr` (recall) `mc` (clear) |
| Other       | `help` `bye` (quit) |

### Defining your own functions

You can create a new function directly in the console using Forth's native syntax:

```
: function-name ... ;
```

Example:

```
: carre fdup * ;
5.0e0 carre        -> 25.0
```

Note: to duplicate the top of the stack, use `fdup` (not `dup`, which operates on a different stack reserved for integers).

Every function you define is automatically saved to `custom_words.fs` (created in the same folder) and will still be available the next time the program is launched.
