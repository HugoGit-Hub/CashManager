import 'package:dio/dio.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class CartService {
  Future<void> addArticleToCart(int idArticle, int quantity) async {
    const FlutterSecureStorage storage = FlutterSecureStorage();
    final String? token = await storage.read(key: 'access_token');

    try {
      await Dio().post(
        'https://g24.epihub.eu:444/api/CartItem/CreateCartItem',
        data: {'idArticle': idArticle, 'quantity': quantity},
        options: Options(
          headers: {
            "Authorization": "Bearer $token",
          },
        ),
      );
    } catch (e) {
      print(e);
    }
  }

  Future<void> deleteCartItemFromCurrentShoppingSessionById(
      String cartitemId) async {
    const FlutterSecureStorage storage = FlutterSecureStorage();
    final String? token = await storage.read(key: "access_token");
    try {
      int id = int.parse(cartitemId);
      await Dio().delete(
          'https://g24.epihub.eu:444/api/ShoppingSession/DeleteCartItemFromCurrentShoppingSession?cartItemId=$id',
          options: Options(headers: {
            "Authorization": "Bearer $token",
          }));
    } catch (e) {
      print(e);
    }
  }

  Future<void> updateCartItemQuantity(
      int id, int newQuantity) async {
    const FlutterSecureStorage storage = FlutterSecureStorage();
    final String? token = await storage.read(key: "access_token");
    try {
      await Dio().put(
          'https://g24.epihub.eu:444/api/ShoppingSession/UpdateCartItemFromCurrentShoppingSession?cartItemId=$id&quantity=$newQuantity',
          options: Options(headers: {
            "Authorization": "Bearer $token",
          }));
    } catch (e) {
      print(e);
    }
  }
}
