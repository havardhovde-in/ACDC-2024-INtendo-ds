interface OrderItem {
  productId: string;
  productName: string;
  quantity: number;
  price: number;
  productImage: string;
  length: number;
}

interface ShippingAddress {
  street: string;
  city: string;
  state: string;
  postalCode: string;
  country: string;
}

export interface Order {
  orderId: string;
  customerEmail: string;
  customerName: string;
  orderDate: string;
  items: OrderItem[];
  totalAmount: number;
  shippingAddress: ShippingAddress;
  status: string;
}
