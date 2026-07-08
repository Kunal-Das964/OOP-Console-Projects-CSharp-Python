from validation import validate_phone, validate_email

test_phones = ["9876543210", "98765", "98765432ab", None]
for p in test_phones:
    print(f"{p} -> {validate_phone(p)}")

test_emails = ["alice@example.com", "alice@example", "alice.com", "a@b.c", None]
for e in test_emails:
    print(f"{e} -> {validate_email(e)}")