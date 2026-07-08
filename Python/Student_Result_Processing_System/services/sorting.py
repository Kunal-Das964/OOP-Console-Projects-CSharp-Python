def sort_students_by_total_marks(students):
    sorted_students = students.copy()
    quick_sort(sorted_students, 0, len(sorted_students)-1)
    
    return sorted_students

def quick_sort(students, low, high):
    if low < high:
        pivot = Patition(students, low, high)
        quick_sort(students, low, pivot - 1)
        quick_sort(students, pivot + 1, high)
        
    return students

def Patition(students, low, high):
    idx = low -1
    for j in range(high):
        if students[j].get_total_marks() > students[high].get_total_marks():
            idx+=1
            students[j], students[idx] = students[idx], students[j]
        
        students[idx+1], students[high] = students[high], students[idx+1]
        
    return idx+1
        
    
     
    