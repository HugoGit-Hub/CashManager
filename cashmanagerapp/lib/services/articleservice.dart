import 'package:dio/dio.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:cashmanagerapp/models/cartitemmodel.dart';
import 'package:cashmanagerapp/models/articlemodel.dart';

class ArticleService {
  Future<List<CartItemModel>> getAllCartItem() async {
    const FlutterSecureStorage storage = FlutterSecureStorage();
    final String? token = await storage.read(key: 'access_token');
    print(token);
    try {
      final res = await Dio()
          .get(
        'http://localhost:5001/api/ShoppingSession/GetCurrentShoppingSession',
        options: Options(
          headers: {
            "Authorization": "Bearer $token",
          },
        ),
      )
          .onError((DioError error, stackTrace) async {
        return Response(
          requestOptions: RequestOptions(path: ''),
          statusCode: error.response?.statusCode ?? 0,
          data: error.response?.data ?? {},
        );
      });
      if (res.statusCode != 200) {
        return [];
      }
      List<CartItemModel> cartitems = [];
      for (var cartitem in res.data) {
        cartitems.add(CartItemModel.fromJson(cartitem));
      }
      return cartitems;
    } catch (e) {
      print(e);
      return [];
    }
  }

  Future<List<ArticleModel>> getAllArticles() async {
    const FlutterSecureStorage storage = FlutterSecureStorage();
    final String? token = await storage.read(key: 'access_token');
    try {
      final res = await Dio()
          .get(
        'http://localhost:5001/api/Article/GetAll',
        options: Options(
          headers: {
            "Authorization": "Bearer $token",
          },
        ),
      )
          .onError((DioError error, stackTrace) async {
        return Response(
          requestOptions: RequestOptions(path: ''),
          statusCode: error.response?.statusCode ?? 0,
          data: error.response?.data ?? {},
        );
      });
      if (res.statusCode != 200) {
        return [];
      }
      List<ArticleModel> articles = [];
      for (var article in res.data) {
        articles.add(ArticleModel.fromJson(article));
      }
      return articles;
    } catch (e) {
      print(e);
      return [];
    }
  }
}
