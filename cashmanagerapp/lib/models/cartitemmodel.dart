class CartItemModel {
  String articleName;
  int quantity;
  double totalArticlePrice;
  String imageUrl;

  CartItemModel(
      {required this.articleName,
      required this.quantity,
      required this.totalArticlePrice,
      required this.imageUrl});

  CartItemModel.fromJson(Map<String, dynamic> json)
      : articleName = json['articleName'] ?? '',
        quantity = json['quantity'] ?? '',
        totalArticlePrice = (json['totalArticlePrice'] as num).toDouble(),
        imageUrl = json['imageUrl'] ?? '';

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = {};
    data['articleName'] = articleName;
    data['quantity'] = quantity;
    data['totalArticlePrice'] = totalArticlePrice.toDouble();
    data['imageUrl'] = imageUrl;
    return data;
  }
}
