from services.error_extractor import extract_error_entries

def count_word_frequency(e_e):
    word_counts = {}
    
    for e in e_e:
        words = e._message.split()
        for word in words:
            cleaned_word = word.lower()
            if cleaned_word not in word_counts:
                word_counts[cleaned_word] = 1
            else:
                word_counts[cleaned_word] += 1
                
    return word_counts

def get_most_frequent_word(w_c):
    if len(w_c) == 0:
        return None
    
    best_word = None
    best_count = 0
    
    for w, c in w_c.items():
        if c > best_count:
            best_count = c
            best_word = w
    return (best_word, best_count)

def print_most_frequent_error_word(e):
    e_e = extract_error_entries(e)
    
    if len(e_e) == 0:
        print("No error messages to analyze")
        return
    
    w_c = count_word_frequency(e_e)
    result = get_most_frequent_word(w_c)
    
    if result is None:
        print("No error messages to analyze.")
        return
    
    w, c = result
    print(f"Most frequent word in ERROR messages: '{w}' ({c} occurrences)")
        