#Сортировка пузырьком #Изменение для Git
def BubbleSort(arr):
    n = len(arr)
    swap_counter = 0
    for i in range(n - 1):
        for j in range(n - 1 - i):
            if arr[j] > arr[j + 1]:
                arr[j], arr[j + 1] = arr[j + 1], arr[j]
                swap_counter += 1
    return swap_counter

#Проведение тестирования
def test(test_num, input_arr, sorted_arr):
    test_arr = input_arr[:] #Копирование массива для тестирования
    swap_count = BubbleSort(test_arr) #Определение числа обменов
    passed = (test_arr == sorted_arr) #Проверка правильности сортировки массива
    return swap_count, passed

#Чтение из файла
def read_file(file_name):
    with open(file_name, 'r') as file:
        #Преобразование строк в список целых чисел
        arr = [list(map(int, line.strip().split())) for line in file]
    return arr
#Ручной ввод
def manual_input():
    n = int(input("Введите количество строк: "))
    arrays = []
    for i in range(n):
        arr = list(map(int, input(f"Введите элементы массива {i + 1} строки через пробел: ").split()))
        arrays.append(arr)
    return arrays

def main():
    choice = input("Выберите источник данных (файл/ввод): ").strip().lower()
    if choice == "файл":
        file_name = 'H:/test_BubbleSort.txt'
        main_arr = read_file(file_name)
    elif choice == "ввод":
        main_arr = manual_input()
    else:
        print("Неверный выбор.")
        return
    total_swap_count = 0 #Для общего количества обменов
    print("\nРезультаты тестирования:")
    #Проведение тестирования по каждому массиву
    for i, arr in enumerate(main_arr):
        sorted_arr = sorted(arr) #Получение ожидаемого отсортированного массива
        swap_count, test_passed = test(i + 1, arr, sorted_arr) #Вызов функции test
        total_swap_count += swap_count #Добавление количества обменов к общему числу
        #Вывод информации о тесте
        print(f"Исходный массив: {' '.join(map(str, arr))} - Тест {i + 1}: {'Да' if test_passed else 'Нет'}")
        print(f"Количество обменов: {swap_count}")
    print("\nОтсортированные массивы:")
    #Сортировка и вывод каждого массива
    for arr in main_arr:
        sorted_arr = arr[:]
        BubbleSort(sorted_arr)
        print(' '.join(map(str, sorted_arr)))
    print(f"\nОбщее количество обменов для сортировки всех массивов: {total_swap_count}")

if __name__ == "__main__":
    main()