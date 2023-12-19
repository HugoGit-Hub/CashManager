import 'dart:convert';
import 'package:dio/dio.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class ArticleService {
  Future<List<Article>> getAllArticles() async {
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
      List<dynamic> data = jsonDecode(res.data);
      List<Article> articles =
          data.map((item) => Article.fromJson(item)).toList();

      return articles;
    } catch (e) {
      print(e);
      return [];
    }
  }
}

class Article {
  final String name;
  final String description;
  final double price;
  final String imageUrl;

  Article(
      {required this.name,
      required this.description,
      required this.price,
      required this.imageUrl});

  factory Article.fromJson(Map<String, dynamic> json) {
    return Article(
      name: json['name'],
      description: json['description'],
      price: json['price'].toDouble(),
      imageUrl: json['imageUrl'],
    );
  }
}
