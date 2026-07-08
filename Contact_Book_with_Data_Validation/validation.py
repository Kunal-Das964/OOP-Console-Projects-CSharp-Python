import re

# Phone rule: digits only, exactly 10 characters long.
def validate_phone(phone):
    if phone is None:
        return False
    return phone.isdigit() and len(phone) == 10


# Email rule (basic pattern, not exhaustive):
# ^[^@\s]+   -> one or more characters that are not '@' or whitespace (the local part)
# @          -> exactly one literal '@'
# [^@\s]+    -> one or more non-'@'/non-whitespace characters (the domain name)
# \.         -> a literal dot
# [^@\s]+$   -> one or more non-'@'/non-whitespace characters after the dot (the extension)
EMAIL_PATTERN = r"^[^@\s]+@[^@\s]+\.[^@\s]+$"

def validate_email(email):
    if email is None:
        return False
    return re.match(EMAIL_PATTERN, email) is not None