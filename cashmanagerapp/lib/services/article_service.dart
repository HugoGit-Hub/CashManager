import 'package:dio/dio.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:cashmanagerapp/models/cartitem_model.dart';
import 'package:cashmanagerapp/models/article_model.dart';

class ArticleService {
  Future<List<CartItemModel>> getAllCartItem() async {
    const FlutterSecureStorage storage = FlutterSecureStorage();
    final String? token = await storage.read(key: 'access_token');
    try {
      final res = await Dio()
          .get(
        'https://g24.epihub.eu:444/api/ShoppingSession/GetCurrentOpenedShoppingSessionCartItems',
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
      for (Map<String, dynamic> cartitem in res.data) {
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
        'https://g24.epihub.eu:444/api/Article/GetAll',
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

  Future<ArticleModel> getArticleById(String idArticle) async {
    const FlutterSecureStorage storage = FlutterSecureStorage();
    final String? token = await storage.read(key: "access_token");

    try {
      int id = int.parse(idArticle);
      final res = await Dio()
          .get(
        'https://g24.epihub.eu:444/api/Article/GetById?id=$id',
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
        print(res.statusCode);
      }
      ArticleModel article = ArticleModel.fromJson(res.data);
      return article;
    } catch (e) {
      print(e);
      return ArticleModel.fromJson({});
    }
  }
}
