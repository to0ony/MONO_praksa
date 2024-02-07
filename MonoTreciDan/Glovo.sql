CREATE TABLE "Role"  (
    "Id" UUID PRIMARY KEY,
    "Name" VARCHAR(50) NOT NULL
);


CREATE TABLE "User" (
    "Id" UUID PRIMARY KEY,
    "RoleId" UUID NOT NULL,
    "FirstName" VARCHAR(50) NOT NULL,
    "LastName" VARCHAR(50) NOT NULL,
    "Email" VARCHAR(100) NOT NULL,
    "Address" VARCHAR(255) NOT NULL,
    CONSTRAINT "FK_User_Role_Id"
    FOREIGN KEY ("RoleId") REFERENCES "Role"("Id")
);

CREATE TABLE "DeliveryStatus" (
    "Id" UUID PRIMARY KEY,
    "StatusName" VARCHAR(50) NOT NULL
);

CREATE TABLE "Restaraunt" (
    "Id" UUID PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "Address" VARCHAR(255) NOT NULL,
    "Email" VARCHAR(100) NOT NULL,
    "OperatingHours" VARCHAR(100) NOT NULL
);

CREATE TABLE "Category" (
	"Id" UUID PRIMARY KEY,
	"Name" VARCHAR(50) NOT NULL
);

CREATE TABLE "Article" (
    "Id" UUID PRIMARY KEY,
    "CategoryId" UUID NOT NULL,
    "Name" VARCHAR(100) NOT NULL,
    "Price" DECIMAL(10, 2) NOT NULL,
    CONSTRAINT "FK_Article_Category_Id"
    FOREIGN KEY ("CategoryId") REFERENCES "Category"("Id")
);

CREATE TABLE "RestarauntArticle" (
    "Id" UUID PRIMARY KEY,
    "RestarauntId" UUID NOT NULL,
    "ArticleId" UUID NOT NULL,
    CONSTRAINT "FK_RestarauntArticle_Restaraunt_Id"
    FOREIGN KEY ("RestarauntId") REFERENCES "Restaraunt"("Id"),
    CONSTRAINT "FK_RestarauntArticle_Article_Id"
    FOREIGN KEY ("ArticleId") REFERENCES "Article"("Id")
);
	
CREATE TABLE "Order" (
    "Id" UUID PRIMARY KEY,
    "DeliverId" UUID NOT NULL,
    "EmployeeId" UUID NOT NULL,
    "CustomerId" UUID NOT NULL,
    "RestarauntId" UUID NOT NULL,
    "DeliveryStatusId" UUID NOT NULL,
    "Date" DATE NOT NULL,
    "Price" DECIMAL(10, 2) NOT NULL,
    CONSTRAINT "FK_Order_DeliverUser_Id"
    FOREIGN KEY ("DeliverId") REFERENCES "User"("Id"),
    CONSTRAINT "FK_Order_EmployeeUser_Id"
    FOREIGN KEY ("EmployeeId") REFERENCES "User"("Id"),
    CONSTRAINT "FK_Order_CustomerUser_Id"
    FOREIGN KEY ("CustomerId") REFERENCES "User"("Id"),
    CONSTRAINT "FK_Order_Restaraunt_Id"
    FOREIGN KEY ("RestarauntId") REFERENCES "Restaraunt"("Id"),
    CONSTRAINT "FK_Order_DeliveryStatus_Id"
    FOREIGN KEY ("DeliveryStatusId") REFERENCES "DeliveryStatus"("Id")
);

CREATE TABLE "OrdersArticles" (
    "OrderId" UUID NOT NULL,
    "RestarauntArticleId" UUID NOT NULL,
    CONSTRAINT "FK_OrdersArticles_Order_Id"
    FOREIGN KEY ("OrderId") REFERENCES "Order"("Id"),
    CONSTRAINT "FK_OrdersArticles_RestarauntArticle_Id"
    FOREIGN KEY ("RestarauntArticleId") REFERENCES "RestarauntArticle"("Id")
);

--INSERT
INSERT INTO public."Role" ("Id", "Name")
VALUES
    ('2a335953-d63f-481f-8bdb-1a37edbf6b0a', 'Employee'),
    ('88619290-ca6c-4dba-85f6-4f7ee4ea211e', 'Consumer'),
    ('00a632e1-4195-4a9a-bf63-35e29cdb0a2c', 'Delivery');

INSERT INTO public."User" ("Id", "RoleId", "FirstName", "LastName", "Email", "Address")
VALUES
    ('2a335953-d63f-481f-8bdb-1a37edbf6b0a', '2a335953-d63f-481f-8bdb-1a37edbf6b0a', 'John', 'Doe', 'john@example.com', '123 Main St'),
    ('88619290-ca6c-4dba-85f6-4f7ee4ea211e', '88619290-ca6c-4dba-85f6-4f7ee4ea211e', 'Jane', 'Smith', 'jane@example.com', '456 Park Ave'),
    ('00a632e1-4195-4a9a-bf63-35e29cdb0a2c', '00a632e1-4195-4a9a-bf63-35e29cdb0a2c', 'Alice', 'Johnson', 'alice@example.com', '789 Elm St')

INSERT INTO public."Restaraunt" ("Id", "Name", "Address", "Email", "OperatingHours")
VALUES
    ('2a335953-d63f-481f-8bdb-1a37edbf6b0a', 'Restoran A', '123 Main St', 'restoran_a@example.com', '08:00-22:00'),
    ('88619290-ca6c-4dba-85f6-4f7ee4ea211e', 'Restoran B', '456 Park Ave', 'restoran_b@example.com', '09:00-23:00'),
    ('00a632e1-4195-4a9a-bf63-35e29cdb0a2c', 'Restoran C', '789 Elm St', 'restoran_c@example.com', '10:00-20:00'),
    ('1ff4281a-0716-4b92-864a-c61883ddbf54', 'Restoran D', '321 Oak St', 'restoran_d@example.com', '07:00-21:00');

INSERT INTO public."DeliveryStatus" ("Id", "StatusName")
VALUES
    ('2a335953-d63f-481f-8bdb-1a37edbf6b0a', 'Processing'),
    ('88619290-ca6c-4dba-85f6-4f7ee4ea211e', 'In transit'),
    ('00a632e1-4195-4a9a-bf63-35e29cdb0a2c', 'Delivered');

INSERT INTO public."Category" ("Id", "Name")
VALUES
    ('2a335953-d63f-481f-8bdb-1a37edbf6b0a', 'Italian'),
    ('88619290-ca6c-4dba-85f6-4f7ee4ea211e', 'Mexican'),
    ('00a632e1-4195-4a9a-bf63-35e29cdb0a2c', 'Japanese');

INSERT INTO public."Order" ("Id", "DeliverId", "EmployeeId", "CustomerId", "RestarauntId", "DeliveryStatusId", "Date", "Price")
VALUES
    ('2a335953-d63f-481f-8bdb-1a37edbf6b0a', '2a335953-d63f-481f-8bdb-1a37edbf6b0a', '88619290-ca6c-4dba-85f6-4f7ee4ea211e', '00a632e1-4195-4a9a-bf63-35e29cdb0a2c', '1ff4281a-0716-4b92-864a-c61883ddbf54', '2a335953-d63f-481f-8bdb-1a37edbf6b0a', '2024-02-07', 50.00);

INSERT INTO public."Article" ("Id", "CategoryId", "Name", "Price")
VALUES
    ('2a335953-d63f-481f-8bdb-1a37edbf6b0a', '2a335953-d63f-481f-8bdb-1a37edbf6b0a', 'Pizza Margherita', 10.00),
    ('88619290-ca6c-4dba-85f6-4f7ee4ea211e', '2a335953-d63f-481f-8bdb-1a37edbf6b0a', 'Spaghetti Carbonara', 12.00),
    ('00a632e1-4195-4a9a-bf63-35e29cdb0a2c', '88619290-ca6c-4dba-85f6-4f7ee4ea211e', 'Taco al Pastor', 8.00),
    ('1ff4281a-0716-4b92-864a-c61883ddbf54', '88619290-ca6c-4dba-85f6-4f7ee4ea211e', 'Burrito Bowl', 9.00),
    ('f58964bc-833f-4bfe-95c5-1cbfb115acb2', '00a632e1-4195-4a9a-bf63-35e29cdb0a2c', 'Sushi Roll', 15.00);

INSERT INTO public."RestarauntArticle" ("Id", "RestarauntId", "ArticleId")
VALUES
    ('2a335953-d63f-481f-8bdb-1a37edbf6b0a', '1ff4281a-0716-4b92-864a-c61883ddbf54', '2a335953-d63f-481f-8bdb-1a37edbf6b0a'),
    ('88619290-ca6c-4dba-85f6-4f7ee4ea211e', '1ff4281a-0716-4b92-864a-c61883ddbf54', '88619290-ca6c-4dba-85f6-4f7ee4ea211e'),
    ('00a632e1-4195-4a9a-bf63-35e29cdb0a2c', '2a335953-d63f-481f-8bdb-1a37edbf6b0a', '00a632e1-4195-4a9a-bf63-35e29cdb0a2c'),
    ('1ff4281a-0716-4b92-864a-c61883ddbf54', '2a335953-d63f-481f-8bdb-1a37edbf6b0a', '1ff4281a-0716-4b92-864a-c61883ddbf54'),
    ('f58964bc-833f-4bfe-95c5-1cbfb115acb2', '00a632e1-4195-4a9a-bf63-35e29cdb0a2c', 'f58964bc-833f-4bfe-95c5-1cbfb115acb2');

INSERT INTO public."OrdersArticles" ("OrderId", "RestarauntArticleId")
VALUES
    ('2a335953-d63f-481f-8bdb-1a37edbf6b0a', '2a335953-d63f-481f-8bdb-1a37edbf6b0a');

