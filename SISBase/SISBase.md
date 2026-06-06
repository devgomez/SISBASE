ESTRUCTURA DEL PROYECTO CLEAN ARQUITECTURE


SISBase
│
├── Presentation
│   │
│   ├── Views
│   │   ├── LoginView.xaml
│   │   ├── DashboardView.xaml
│   │   └── Pages
│   │       ├── CustomerView.xaml
│   │       ├── ProductView.xaml
│   │       └── SalesView.xaml
│   │
│   ├── ViewModels
│   │   ├── BaseViewModel.cs
│   │   ├── CustomerViewModel.cs
│   │   └── ProductViewModel.cs
│   │
│   └── Commands
│       └── RelayCommand.cs
│
├── Domain
│   │
│   ├── Entities
│   │   ├── Customer.cs
│   │   ├── Product.cs
│   │   └── User.cs
│   │
│   ├── Interfaces
│   │   ├── ICustomerRepository.cs
│   │   ├── IProductRepository.cs
│   │   └── IUserRepository.cs
│   │
│   └── Services
│
├── Infrastructure
│   │
│   ├── Persistence
│   │   │
│   │   ├── Sqlite
│   │   │   ├── AppDbContext.cs
│   │   │   ├── CustomerRepository.cs
│   │   │   └── ProductRepository.cs
│   │   │
│   │   └── Api
│   │       ├── CustomerApiRepository.cs
│   │       ├── ProductApiRepository.cs
│   │       └── HttpClientFactory.cs
│   │
│   └── DependencyInjection
│       └── ServiceRegistration.cs
│
├── Configuration
│   └── appsettings.json
│
└── App.xaml.cs


FUNCIONALIDADES DEL SISTEMA
1. LOGIN
1. USUARIOS
1. ROLES
1. MENU
1. OPCIONES
1. CONFIGURACION DE NOMBRES Y LOGO DEL SISTEMA
1. CATEGORIAS
1. MARCAS
1. Tipos de comprobante
1. Tipos de documento de identidad
1. Unidades SUNAT






SQL SCRIPT
PRAGMA foreign_keys = ON;

-- ==========================================
-- EMPRESA
-- ==========================================

CREATE TABLE companies (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    ruc TEXT NOT NULL UNIQUE,
    razon_social TEXT NOT NULL,
    nombre_comercial TEXT,
    direccion TEXT,
    ubigeo TEXT,
    distrito TEXT,
    provincia TEXT,
    departamento TEXT,
    telefono TEXT,
    email TEXT,
    certificado_digital TEXT,
    usuario_sol TEXT,
    clave_sol TEXT,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- ==========================================
-- USUARIOS Y ROLES
-- ==========================================

CREATE TABLE roles (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    nombre TEXT NOT NULL UNIQUE
);

CREATE TABLE users (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    username TEXT NOT NULL UNIQUE,
    password_hash TEXT NOT NULL,
    nombres TEXT NOT NULL,
    apellidos TEXT,
    email TEXT,
    activo INTEGER DEFAULT 1,
    role_id INTEGER,
    FOREIGN KEY(role_id) REFERENCES roles(id)
);

-- ==========================================
-- CLIENTES
-- ==========================================

CREATE TABLE customers (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    tipo_documento TEXT NOT NULL,
    numero_documento TEXT NOT NULL UNIQUE,
    nombres TEXT NOT NULL,
    direccion TEXT,
    telefono TEXT,
    email TEXT,
    activo INTEGER DEFAULT 1,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- ==========================================
-- PROVEEDORES
-- ==========================================

CREATE TABLE suppliers (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    ruc TEXT NOT NULL UNIQUE,
    razon_social TEXT NOT NULL,
    direccion TEXT,
    telefono TEXT,
    email TEXT,
    contacto TEXT,
    activo INTEGER DEFAULT 1
);

-- ==========================================
-- MARCAS
-- ==========================================

CREATE TABLE brands (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    nombre TEXT NOT NULL UNIQUE
);

-- ==========================================
-- CATEGORIAS
-- ==========================================

CREATE TABLE categories (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    nombre TEXT NOT NULL,
    descripcion TEXT
);

-- ==========================================
-- UNIDADES MEDIDA SUNAT
-- ==========================================

CREATE TABLE units (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    codigo_sunat TEXT NOT NULL,
    nombre TEXT NOT NULL
);

-- ==========================================
-- PRODUCTOS
-- ==========================================

CREATE TABLE products (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    codigo TEXT NOT NULL UNIQUE,
    codigo_barras TEXT,
    descripcion TEXT NOT NULL,
    categoria_id INTEGER,
    marca_id INTEGER,
    unidad_id INTEGER,
    precio_compra DECIMAL(12,2),
    precio_venta DECIMAL(12,2),
    stock_minimo DECIMAL(12,2) DEFAULT 0,
    afectacion_igv TEXT DEFAULT '10',
    activo INTEGER DEFAULT 1,

    FOREIGN KEY(categoria_id) REFERENCES categories(id),
    FOREIGN KEY(marca_id) REFERENCES brands(id),
    FOREIGN KEY(unidad_id) REFERENCES units(id)
);

-- ==========================================
-- ALMACENES
-- ==========================================

CREATE TABLE warehouses (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    nombre TEXT NOT NULL,
    direccion TEXT
);

-- ==========================================
-- STOCK
-- ==========================================

CREATE TABLE product_stock (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    product_id INTEGER NOT NULL,
    warehouse_id INTEGER NOT NULL,
    stock_actual DECIMAL(12,2) DEFAULT 0,

    UNIQUE(product_id, warehouse_id),

    FOREIGN KEY(product_id) REFERENCES products(id),
    FOREIGN KEY(warehouse_id) REFERENCES warehouses(id)
);

-- ==========================================
-- MOVIMIENTOS INVENTARIO
-- ==========================================

CREATE TABLE inventory_movements (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    product_id INTEGER NOT NULL,
    warehouse_id INTEGER NOT NULL,
    tipo TEXT NOT NULL,
    cantidad DECIMAL(12,2) NOT NULL,
    costo_unitario DECIMAL(12,2),
    observacion TEXT,
    fecha DATETIME DEFAULT CURRENT_TIMESTAMP,

    FOREIGN KEY(product_id) REFERENCES products(id),
    FOREIGN KEY(warehouse_id) REFERENCES warehouses(id)
);

-- ==========================================
-- COMPRAS
-- ==========================================

CREATE TABLE purchases (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    supplier_id INTEGER NOT NULL,
    fecha DATE NOT NULL,
    tipo_comprobante TEXT,
    serie TEXT,
    numero TEXT,
    subtotal DECIMAL(12,2),
    igv DECIMAL(12,2),
    total DECIMAL(12,2),

    FOREIGN KEY(supplier_id) REFERENCES suppliers(id)
);

CREATE TABLE purchase_details (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    purchase_id INTEGER NOT NULL,
    product_id INTEGER NOT NULL,
    cantidad DECIMAL(12,2),
    precio_unitario DECIMAL(12,2),
    subtotal DECIMAL(12,2),

    FOREIGN KEY(purchase_id) REFERENCES purchases(id),
    FOREIGN KEY(product_id) REFERENCES products(id)
);

-- ==========================================
-- SERIES DOCUMENTOS
-- ==========================================

CREATE TABLE document_series (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    tipo_documento TEXT NOT NULL,
    serie TEXT NOT NULL,
    correlativo INTEGER DEFAULT 1,
    activo INTEGER DEFAULT 1
);

-- ==========================================
-- VENTAS
-- ==========================================

CREATE TABLE sales (
    id INTEGER PRIMARY KEY AUTOINCREMENT,

    customer_id INTEGER NOT NULL,

    tipo_documento TEXT NOT NULL,
    serie TEXT NOT NULL,
    numero TEXT NOT NULL,

    fecha_emision DATETIME NOT NULL,

    subtotal DECIMAL(12,2),
    igv DECIMAL(12,2),
    total DECIMAL(12,2),

    moneda TEXT DEFAULT 'PEN',
    estado TEXT DEFAULT 'EMITIDO',

    hash_cpe TEXT,
    xml_path TEXT,
    cdr_path TEXT,

    FOREIGN KEY(customer_id) REFERENCES customers(id)
);

CREATE TABLE sale_details (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    sale_id INTEGER NOT NULL,
    product_id INTEGER NOT NULL,

    cantidad DECIMAL(12,2),
    precio_unitario DECIMAL(12,2),
    descuento DECIMAL(12,2) DEFAULT 0,
    subtotal DECIMAL(12,2),

    FOREIGN KEY(sale_id) REFERENCES sales(id),
    FOREIGN KEY(product_id) REFERENCES products(id)
);

-- ==========================================
-- FACTURACION ELECTRONICA
-- ==========================================

CREATE TABLE electronic_documents (
    id INTEGER PRIMARY KEY AUTOINCREMENT,

    sale_id INTEGER NOT NULL,

    tipo_documento TEXT,
    serie TEXT,
    numero TEXT,

    ticket_sunat TEXT,
    codigo_respuesta TEXT,
    descripcion_respuesta TEXT,

    estado_envio TEXT,

    fecha_envio DATETIME,
    fecha_respuesta DATETIME,

    xml_firmado TEXT,
    xml_enviado TEXT,
    cdr_zip TEXT,

    FOREIGN KEY(sale_id) REFERENCES sales(id)
);

-- ==========================================
-- NOTAS DE CREDITO
-- ==========================================

CREATE TABLE credit_notes (
    id INTEGER PRIMARY KEY AUTOINCREMENT,

    sale_id INTEGER NOT NULL,

    serie TEXT,
    numero TEXT,

    motivo TEXT,
    subtotal DECIMAL(12,2),
    igv DECIMAL(12,2),
    total DECIMAL(12,2),

    fecha_emision DATETIME,

    FOREIGN KEY(sale_id) REFERENCES sales(id)
);

-- ==========================================
-- CAJA
-- ==========================================

CREATE TABLE cash_registers (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    nombre TEXT NOT NULL
);

CREATE TABLE cash_movements (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    caja_id INTEGER,
    fecha DATETIME DEFAULT CURRENT_TIMESTAMP,
    tipo TEXT,
    concepto TEXT,
    monto DECIMAL(12,2),

    FOREIGN KEY(caja_id) REFERENCES cash_registers(id)
);

-- ==========================================
-- FORMAS DE PAGO
-- ==========================================

CREATE TABLE payment_methods (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    nombre TEXT NOT NULL
);

CREATE TABLE sale_payments (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    sale_id INTEGER,
    payment_method_id INTEGER,
    monto DECIMAL(12,2),

    FOREIGN KEY(sale_id) REFERENCES sales(id),
    FOREIGN KEY(payment_method_id) REFERENCES payment_methods(id)
);

-- ==========================================
-- CONFIGURACIONES
-- ==========================================

CREATE TABLE settings (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    clave TEXT UNIQUE,
    valor TEXT
);











