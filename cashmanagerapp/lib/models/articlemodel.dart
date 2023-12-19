class ArticleModel {
  int id;
  String name;
  String description;
  double price;
  String imageUrl;

  ArticleModel(
      {required this.id,
      required this.name,
      required this.description,
      required this.price,
      required this.imageUrl});

  ArticleModel.fromJson(Map<String, dynamic> json)
      : id = json['id'] ?? 0,
        name = json['name'] ?? '',
        description = json['description'] ?? '',
        price = (json['price'] as num).toDouble(),
        imageUrl = json['imageUrl'] ?? '';

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = {};
    data['id'] = id;
    data['name'] = name;
    data['description'] = description;
    data['price'] = price.toDouble();
    data['imageUrl'] = imageUrl;
    return data;
  }
}
