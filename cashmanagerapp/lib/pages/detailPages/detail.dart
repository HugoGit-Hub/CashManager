import 'package:cashmanagerapp/models/article_model.dart';
import 'package:cashmanagerapp/services/article_service.dart';
import 'package:flutter/material.dart';
import 'package:cashmanagerapp/widgets/button_like.dart';
import 'package:cashmanagerapp/widgets/quantity_selector.dart';
import 'package:cashmanagerapp/widgets/button_add_to_cart.dart';

class Detail extends StatefulWidget {
  Detail({Key? key, required this.idArticle, required this.quantity}) : super(key: key);

  final String? idArticle;
  final int quantity;
  @override
  State<StatefulWidget> createState() => _DetailState();
}

class _DetailState extends State<Detail> {
  ArticleModel? article;
  double totalPrice = 0;
  late int totalquantity;
  @override
  void initState() {
    super.initState();
    totalquantity = widget.quantity;
    ArticleService().getArticleById(widget.idArticle!).then((value) {
      setState(() {
        article = value;
        totalPrice = (article?.price  ?? 0) * widget.quantity; 
      });
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Detail page'),
      ),
      body: Container(
        decoration: BoxDecoration(
          image: DecorationImage(
            image: AssetImage(article?.imageUrl ?? 'lib/images/unknown.jpg'),
            fit: BoxFit.cover,
          ),
        ),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.end,
          children: [
            Container(
              decoration: BoxDecoration(
                color: Colors.white,
                borderRadius: BorderRadius.only(
                  topLeft: Radius.circular(40.0),
                  topRight: Radius.circular(40.0),
                ),
              ),
              padding: EdgeInsets.all(16.0),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    article?.name ?? 'unknown',
                    style: TextStyle(
                      fontWeight: FontWeight.bold,
                      fontSize: 24.0,
                    ),
                  ),
                  SizedBox(height: 10.0),
                  Row(mainAxisAlignment: MainAxisAlignment.center, children: [
                    Text(
                      '${article?.price.toString() ?? 'unknown'} €/unité',
                      style: TextStyle(
                        fontWeight: FontWeight.bold,
                        fontSize: 16.0,
                        color: Colors.yellow[600],
                      ),
                    ),
                    SizedBox(width: 60),
                    QuantitySelector(
                      price: article?.price ?? 0,
                      onQuantityChanged: (quantity, total) {
                        setState(() {
                          totalquantity = quantity;
                          totalPrice = total;
                        });
                      }, 
                      quantity: totalquantity,
                    ),
                    SizedBox(width: 10),
                  ]),
                  SizedBox(height: 15.0),
                  Text(
                    'Description',
                    style: TextStyle(
                      fontWeight: FontWeight.w900,
                      fontSize: 16.0,
                    ),
                  ),
                  SizedBox(height: 10.0),
                  SizedBox(
                    height: 100.0,
                    child: ListView(
                      scrollDirection: Axis.vertical,
                      children: [
                        Text(
                          article?.description ?? 'unknown',
                          style: TextStyle(
                            fontSize: 16.0,
                          ),
                        ),
                      ],
                    ),
                  ),
                  SizedBox(height: 15.0),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      ButtonLike(),
                      SizedBox(width: 10.0),
                      ButtonAddToCart(
                          totalPrice: totalPrice,
                          idArticle: article?.id ?? 0,
                          quantity: totalquantity),
                    ],
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}
