# Alg2DuctTape

Програма, яка на основі декількох C/C++ файлів створює один, який придатний до здачі на Алгоритмах 2.

### Інструкція використання
1. Спочатку потрібно клонувати цей репозиторій. [Детальна інструкція для Visual Studio 2019](https://learn.microsoft.com/en-us/visualstudio/version-control/git-clone-repository?view=vs-2019).
2. В ```Program.cs```(5-12 рядки) потрібно ввести свої дані:
```csharp
// Numer albumu:
const string StudentId = "53xxx";
// Numer grupy laboratoryjnej:
const string GroupName = "200X";
// Imię i nazwisko autora:
const string AuthorFullName = "Imię Nazwisko";
// Adres email:
const string Email = "in53xxx@zut.edu.pl";
```
3. Далі запускаємо програму:
   * В 'Project directory path: ' вводимо шлях до папки, яка містить головний .cpp/.c файл
   * В 'Lab name: ' вводимо назву лабораторної роботи(напр. ```Lab01```)

Після цих кроків шлях до створеного файлу та тема email'у будуть виведені на екран.

### Увага!
Програма була створена за допомогою Visual Studio 2022, тому можливі проблеми сумісності з Visual Studio 2019. При таких проблемах можна створити новий проєкт у Visual Studio 2019 та скопіювати код з ```Program.cs```.
