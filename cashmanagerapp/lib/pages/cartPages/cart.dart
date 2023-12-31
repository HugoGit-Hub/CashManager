import 'package:cashmanagerapp/models/cartitem_model.dart';
import 'package:cashmanagerapp/models/shopping_session_model.dart';
import 'package:cashmanagerapp/services/article_service.dart';
import 'package:cashmanagerapp/services/cart_service.dart';
import 'package:cashmanagerapp/services/shopping_session_service.dart';
import 'package:cashmanagerapp/widgets/button_place_order.dart';
import 'package:cashmanagerapp/widgets/quantity_selector.dart';
import 'package:flutter/material.dart';
import 'dart:math';

class Cart extends StatefulWidget {
  Cart({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _CartState();
}

class _CartState extends State<Cart> {
  List<CartItemModel> cartitems = [];
  double totalPrice = 0.00;

  @override
  void initState() {
    super.initState();
    _loadData();
  }

  Future<void> _loadData() async {
    await _getAllCartItem();
    await _getTotalPrice();
  }

  Future<void> _getAllCartItem() async {
    final List<CartItemModel> cartitems =
        await ArticleService().getAllCartItem();
    setState(() {
      this.cartitems = cartitems;
    });
  }

  Future<void> _getTotalPrice() async {
    final ShoppingSessionModel totalPrice =
        await ShoppingSessionService().getTotalPrice();
    setState(() {
      this.totalPrice = totalPrice.totalPrice;
    });
  }

  double roundDouble(double value, int places){ 
   num mod = pow(10.0, places); 
   return ((value * mod).round().toDouble() / mod); 
}

  Future<void> _deleteCartItem(String cartItemId) async {
    await CartService()
        .deleteCartItemFromCurrentShoppingSessionById(cartItemId);
    await _loadData(); // Reload data after deletion
  }

  void _updateTotalPrice() {
    double newTotalPrice = 0.0;
    for (var item in cartitems) {
      newTotalPrice += item.totalArticlePrice;
    }
    setState(() {
      totalPrice = roundDouble(newTotalPrice, 2);
    });
  }
  void _updateQuandity(int cartItemId, int quantity) {
    CartService().updateCartItemQuantity(cartItemId, quantity);
  }
  
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      // appBar: AppBar(
      //   title: Text('Cart'),
      // ),
      body: Column(
        children: [
          Padding(
            padding: const EdgeInsets.all(8.0),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Text(
                  'Détail des articles'.toUpperCase(),
                  style: TextStyle(
                    fontWeight: FontWeight.bold,
                    fontSize: 20.0,
                  ),
                ),
              ],
            ),
          ),
          Expanded(
            child: ListView.builder(
              itemCount: cartitems.length,
              itemBuilder: (context, index) {
                var cartitem = cartitems[index];
                return Padding(
                  padding: const EdgeInsets.all(8.0),
                  child: Row(
                    children: [
                      Padding(
                        padding: const EdgeInsets.all(16.0),
                        child: Container(
                          width: 50.0,
                          height: 70.0,
                          decoration: BoxDecoration(
                            image: DecorationImage(
                              image: AssetImage(cartitem.imageUrl),
                              fit: BoxFit.cover,
                            ),
                          ),
                        ),
                      ),
                      Expanded(
                        child: Padding(
                          padding: const EdgeInsets.all(8.0),
                          child: Column(
                            mainAxisAlignment: MainAxisAlignment.spaceBetween,
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Text(
                                cartitem.articleName,
                                style: TextStyle(
                                  fontWeight: FontWeight.bold,
                                  fontSize: 18.0,
                                ),
                              ),
                              Text(
                                '${roundDouble(cartitem.totalArticlePrice, 2)} €',
                                style: TextStyle(
                                  fontWeight: FontWeight.bold,
                                  fontSize: 18.0,
                                ),
                              ),
                            ],
                          ),
                        ),
                      ),
                      Row(
                        children: [
                          
                          QuantitySelector(
                            
                            price: cartitem.totalArticlePrice / cartitem.quantity , // Change this to the actual price of your item
                            onQuantityChanged: (quantity, total) {
                              // Update the quantity and total price for the specific item
                              setState(() {
                                cartitem.quantity = quantity;
                                cartitem.totalArticlePrice = total;
                              });

                              // Recalculate the total price for the entire cart
                              _updateTotalPrice();
                              _updateQuandity(cartitem.id, cartitem.quantity);
                            },
                            quantity: cartitem.quantity,
                          ),
                        ],
                      ),
                      Row(
                        children: [
                          IconButton(
                            onPressed: () {
                              _deleteCartItem(cartitem.id.toString());
                            },
                            icon: Icon(Icons.delete),
                          ),
                        ],
                      ),
                    ],
                  ),
                );
              },
            ),
          ),
          Divider(
            thickness: 3.0,
            height: 1.0,
            color: Colors.grey[400],
          ),
          SizedBox(height: 10.0),
          Padding(
            padding: const EdgeInsets.symmetric(vertical: 10.0),
            child: Text(
              'Ajoutez des articles d\'une valeur supplémentaire de 5€ pour une livraison GRATUITE',
              style: TextStyle(
                fontWeight: FontWeight.bold,
                fontSize: 14.0,
                color: Colors.green,
              ),
              textAlign: TextAlign.center,
            ),
          ),
          Padding(
            padding: const EdgeInsets.only(
              bottom: 16.0,
            ),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceAround,
              children: [
                Column(
                  children: [
                    Text(
                      'Total : $totalPrice : €',
                      style: TextStyle(
                        fontWeight: FontWeight.bold,
                        fontSize: 20.0,
                      ),
                    ),
                  ],
                ),
                ButtonPlaceOrder(),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
