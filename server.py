import socket
import psycopg2
import json

sock = socket.socket()
sock.bind(('', 7000))
sock.listen(1)
# conn, addr = sock.accept()

con = psycopg2.connect(
    host="localhost",
    database="postgres",
    user="postgres",
    password="postgres",
    port=5433
)


# работает
# проверяет есть такой пользователь с логином и паролем,
# если есть, отправляет его роль приложению, если нет, говорит что пользователя нет
def authorization(login, password):
    cur = con.cursor()
    cur.execute("SELECT count(*) FROM public.user WHERE login = %(login)s AND password = %(password)s",
                {'login': login, 'password': password})
    rows = cur.fetchall()
    # print(rows)
    if rows != [(0,)]:
        cur.execute("SELECT role FROM public.user WHERE login = %(login)s AND password = %(password)s",
                    {'login': login, 'password': password})
        rows = cur.fetchall()
        # print(rows)
    else:
        rows = "Неверный логин или пароль"
    return (json.dumps(rows, ensure_ascii=False)).encode('UTF-16')


# проверяет есть такой пользователь с логином и паролем,
# если есть, говорит, что пользователь уже есть, если нет, добавляет
def registration(login, password, name):
    cur = con.cursor()
    cur.execute("SELECT count(*) FROM public.user WHERE login = %(login)s",
                {'login': login})
    rows = cur.fetchall()
    if rows != [(0,)]:
        rows = "Этот логин уже кто-то использует"
    else:
        cur.execute("INSERT INTO public.user VALUES %(login)s, %(password)s, %(name)s, заказчик)",
                    {'login': login, 'password': password, 'name': name})
        cur.commit()
        rows = "Пользователь успешно добавлен"
    return (json.dumps(rows, ensure_ascii=False)).encode('UTF-16')


# выводит список ткани на складе
def inventory_cloth():
    cur = con.cursor()
    cur.execute("SELECT id_cloth, quantity_roll FROM fabric_warehouse")
    rows = cur.fetchall()
    return (json.dumps(rows, ensure_ascii=False)).encode('UTF-16')


# выводит список фурнитуры на складе
def inventory_fittings():
    cur = con.cursor()
    cur.execute("SELECT id_fittings, quantity FROM hardware_warehouse")
    rows = cur.fetchall()
    return (json.dumps(rows, ensure_ascii=False)).encode('UTF-16')


# изменяет кол-во фурнитуры на складе
def change_fittings(id_fittings, quantity):
    cur = con.cursor()
    cur.execute("UPDATE hardware_warehouse SET quantity = %(quantity)s WHERE id_fittings = %(id_fittings)s",
                {"quantity": quantity, "id_fittings": id_fittings})
    cur.commit()
    return (inventory_fittings()).encode('UTF-16')


# изменяет кол-во рулонов ткани на складе
def change_cloth(id_cloth, quantity_roll):
    cur = con.cursor()
    cur.execute("UPDATE fabric_warehouse SET quantity_roll = %(quantity_roll)s WHERE id_cloth = %(id_cloth)s",
                {"quantity_roll": quantity_roll, "id_cloth": id_cloth})
    cur.commit()
    return (inventory_cloth()).encode('UTF-16')


# выводит список ткани
def list_cloth():
    cur = con.cursor()
    cur.execute("SELECT id_cloth, length, price FROM Cloth")
    rows = cur.fetchall()
    print(rows)
    return (json.dumps(rows, ensure_ascii=False)).encode('UTF-16')


# выводит список фурнитуры
def list_fittings():
    cur = con.cursor()
    cur.execute("SELECT id_fittings, width, price FROM Fittings")
    rows = cur.fetchall()
    print(rows)
    return (json.dumps(rows, ensure_ascii=False)).encode('UTF-16')


# высчитавает цену всех тканей
def purchase_price_cloth():
    cur = con.cursor()
    cur.execute(
        "SELECT sum(price * quantity_roll) FROM Cloth JOIN fabric_warehouse ON (Cloth.id_cloth = fabric_warehouse.id_cloth)")
    rows = cur.fetchall()
    return (json.dumps(rows, ensure_ascii=False)).encode('UTF-16')


# высчитавает цену всей фурнитуры
def purchase_price_fittings():
    cur = con.cursor()
    cur.execute(
        "SELECT sum(price * quantity) FROM Fittings JOIN hardware_warehouse ON (Fittings.id_fittings = hardware_warehouse.id_fittings)")
    rows = cur.fetchall()
    return (json.dumps(rows, ensure_ascii=False)).encode('UTF-16')


# добавляет одну строку фурнитуры и добавляет её на склад
def addition_fittings(id_fittings, name, image, width, price):
    cur = con.cursor()
    cur.execute("SELECT count() FROM Fittings WHERE id_fittings = %(id_fittings)s",
                {"id_fittings": id_fittings})
    rows = cur.fetchall()
    if rows != 0:
        rows = "Этот ID фурнитуры уже есть на складе"
    else:
        cur.execute(
            "INSERT INTO Fittings VALUES %(id_fittings)s, %(name)s, %(image)s, %(width)s, %(price)s)",
            {"id_fittings": id_fittings, "name": name, "image": image, "width": width, "price": price})
        cur.commit()
        cur.execute("SELECT max(id_batch) FROM hardware_warehouse")
        rows = cur.fetchall()
        cur.execute(
            "INSERT INTO hardware_warehouse VALUES %(id_batch)s, %(id_fittings)s, 1",
            {"id_batch": rows + 1, "id_fittings": id_fittings})
        cur.commit()
        rows = "Данные успешно добавлены"
    return (json.dumps(rows, ensure_ascii=False)).encode('UTF-16')


# добавляет одну строку ткани и добавляет её на склад
def addition_cloth(id_cloth, name, colour, picture, image, composition, length, price):
    cur = con.cursor()
    cur.execute("SELECT count() FROM cloth WHERE id_cloth = %(id_cloth)s",
                {"id_cloth": id_cloth})
    rows = cur.fetchall()
    if rows != 0:
        rows = "Этот ID ткани уже есть на складе"
    else:
        cur.execute(
            "INSERT INTO cloth VALUES %(id_cloth)s, %(name)s, %(colour)s, %(picture)s, %(image)s, %(composition)s, %(length)s, %(price)s)",
            {"id_cloth": id_cloth, "name": name, "colour": colour, "picture": picture, "image": image,
             "composition": composition, "length": length, "price": price})
        cur.commit()
        cur.execute("SELECT max(id_roll) FROM fabric_warehouse")
        rows = cur.fetchall()
        cur.execute(
            "INSERT INTO fabric_warehouse VALUES %(id_roll)s, %(id_cloth)s, 1",
            {"id_roll": rows + 1, "id_cloth": id_cloth})
        cur.commit()
        rows = "Данные успешно добавлены"
    return (json.dumps(rows, ensure_ascii=False)).encode('UTF-16')


# удаляет одну строку фурнитуры и удаляет её со склада
def write_off_fittings(id_fittings):
    cur = con.cursor()
    cur.execute("SELECT count() FROM Fittings WHERE id_fittings = %(id_fittings)s",
                {"id_fittings": id_fittings})
    rows = cur.fetchall()
    if rows != 0:
        rows = "Этого ID фурнитуры нет на складе"
    else:
        cur.execute("DELETE FROM Fittings WHERE id_fittings = %(id_fittings)s",
                {"id_fittings": id_fittings})
        cur.commit()
        cur.execute("DELETE FROM Fithardware_warehousetings WHERE id_fittings = %(id_fittings)s",
                    {"id_fittings": id_fittings})
        cur.commit()
        rows = "Данные успешно удаленны"
    return (json.dumps(rows, ensure_ascii=False)).encode('UTF-16')


# удаляет одну строку ткани и удаляет её со склада
def write_off_cloth(id_cloth):
    cur = con.cursor()
    cur.execute("SELECT count() FROM cloth WHERE id_cloth = %(id_cloth)s",
                {"id_cloth": id_cloth})
    rows = cur.fetchall()
    if rows != 0:
        rows = "Этого ID ткани нет на складе"
    else:
        cur.execute("DELETE FROM cloth WHERE id_cloth = %(id_cloth)s",
                    {"id_cloth": id_cloth})
        cur.commit()
        cur.execute("DELETE FROM fabric_warehouse WHERE id_cloth = %(id_cloth)s",
                    {"id_cloth": id_cloth})
        cur.commit()
        rows = "Данные успешно удаленны"
    return (json.dumps(rows, ensure_ascii=False)).encode('UTF-16')


# выводит список изделий(товаров)
def product_list():
    cur = con.cursor()
    cur.execute("SELECT picture, name, width, length FROM Product")
    rows = cur.fetchall()
    return (json.dumps(rows, ensure_ascii=False)).encode('UTF-16')


# выводит список заказов для заказчика
def order_list(login):
    cur = con.cursor()
    cur.execute(
        "SELECT picture, name, date, quantity FROM 'order' JOIN Ordered_products ON ('order'.id_order = Ordered_products.id_order) JOIN Product ON (Ordered_products.id_product = Product.id_product) JOIN 'user' ON (customer = login) WHERE login = %(login)s",
        {"login": login})
    rows = cur.fetchall()
    return (json.dumps(rows, ensure_ascii=False)).encode('UTF-16')


# добавляет заказ
def adding_order(date, customer, id_product):
    cur = con.cursor()
    cur.execute("SELECT max(id_order) FROM 'order'")
    rows1 = cur.fetchall()
    cur.execute("SELECT price FROM product WHERE id_product = %(id_product)s",
                {"id_product": id_product})
    rows2 = cur.fetchall()
    cur.execute(
        "INSERT INTO 'order' VALUES %(id_order)s, %(date)s, Новый, %(customer)s, %(price)s)",
        {"id_order": rows1 + 1, "date": date, "customer": customer, "price": rows2})
    cur.commit()
    rows = "Заказ успешно добавлен"
    return (json.dumps(rows, ensure_ascii=False)).encode('UTF-16')


#
#
#
#
#
#
#
#
#
#
#
#
#
#


while True:
    conn, addr = sock.accept()

    data = conn.recv(1024)
    result = data.decode('UTF-16').split("/")
    if not data:
        continue

    # запросы авторизации и регистрации
    if result[0] == "get_authorization":
        conn.sendall(authorization(result[1], result[2]))
    if result[0] == "get_registration":
        conn.sendall(registration(result[1], result[2], result[3]))

    # запросы инвентаризации
    if result[0] == "get_inventory_cloth":
        conn.sendall(inventory_cloth())
    if result[0] == "get_cloth_change":
        conn.sendall(change_cloth(result[1], result[2]))
    if result[0] == "get_inventory_fittings":
        conn.sendall(inventory_fittings())
    if result[0] == "get_fittings_change":
        conn.sendall(change_fittings(result[1], result[2]))

    # запросы на проверку списка материалов
    if result[0] == "get_list_cloth":
        conn.sendall(list_cloth())
    if result[0] == "get_list_fittings":
        conn.sendall(list_fittings())

    # запросы добавления и списания
    if result[0] == "get_addition_fittings":
        conn.sendall(addition_fittings(result[1], result[2], result[3], result[4], result[5]))
    if result[0] == "get_addition_cloth":
        conn.sendall(
            addition_cloth(result[1], result[2], result[3], result[4], result[5], result[6], result[7], result[8]))
    if result[0] == "get_write_off_fittings":
        conn.sendall(write_off_fittings(result[1]))
    if result[0] == "get_write_off_cloth":
        conn.sendall(write_off_cloth(result[1]))

    # запрос на расчёт закупочной стоимости
    if result[0] == "get_purchase_price_fittings":
        conn.sendall(purchase_price_fittings())
    if result[0] == "get_purchase_price_cloth":
        conn.sendall(purchase_price_cloth())

    # запрос на вывод списка изделий
    if result[0] == "get_product_list":
        conn.sendall(product_list())

    # запрос на вывод списка заказов для заказчика
    if result[0] == "get_order_list":
        conn.sendall(order_list(result[1]))

    # запрос на добавления заказа
    if result[0] == "get_adding_order":
        conn.sendall(adding_order(result[1], result[2], result[3]))

    #


conn.close()
