class Contact:
    def __init__(self, name, phone, email, category):
        self._name = name
        self._phone = phone
        self._email = email
        self._category = category

    def display_info(self):
        print(f"Name: {self._name} | Phone: {self._phone} | Email: {self._email} | Category: {self._category}")