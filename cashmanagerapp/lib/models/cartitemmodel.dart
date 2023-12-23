class CartItemModel {
  String articleName;
  int quantity;
  double totalArticlePrice;
  String imageUrl;
  int articleId;
  CartItemModel(
      {required this.articleName,
      required this.quantity,
      required this.totalArticlePrice,
      required this.imageUrl,
      required this.articleId});

  CartItemModel.fromJson(Map<String, dynamic> json)
      : articleName = json['articleName'] ?? '',
        quantity = json['quantity'] ?? '',
        totalArticlePrice = (json['totalArticlePrice'] as num).toDouble(),
        imageUrl = json['imageUrl'] ?? '',
        articleId = json['articleId'] ?? 0;

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = {};
    data['articleName'] = articleName;
    data['quantity'] = quantity;
    data['totalArticlePrice'] = totalArticlePrice.toDouble();
    data['imageUrl'] = imageUrl;
    data['articleId'] = articleId;
    return data;
  }
}
