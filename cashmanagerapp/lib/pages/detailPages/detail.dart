import 'package:flutter/material.dart';
import 'package:cashmanagerapp/widgets/button_like.dart';
import 'package:cashmanagerapp/widgets/quantity_selector.dart';
import 'package:cashmanagerapp/widgets/button_add_to_cart.dart';

class Detail extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Detail page'),
      ),
      body: Stack(
        children: <Widget>[
          Image.asset(
            'lib/images/top-view-raw-potatoes-table.jpg',
            fit: BoxFit.cover,
            height: double.infinity,
            width: double.infinity,
          ),
          Positioned(
            bottom: 0,
            left: 0,
            right: 0,
            child: Container(
              padding: EdgeInsets.all(16.0),
              decoration: BoxDecoration(
                color: Colors.white,
                borderRadius: BorderRadius.only(
                  topLeft: Radius.circular(30.0),
                  topRight: Radius.circular(30.0),
                ),
              ),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    'Pommes de terre fraiches',
                    style: TextStyle(
                      fontWeight: FontWeight.bold,
                      fontSize: 24.0,
                    ),
                  ),
                  SizedBox(height: 10.0),
                  Row(mainAxisAlignment: MainAxisAlignment.center, children: [
                    Text(
                      '5€/kg',
                      style: TextStyle(
                        fontWeight: FontWeight.bold,
                        fontSize: 16.0,
                        color: Colors.yellow[600],
                      ),
                    ),
                    SizedBox(width: 60),
                    QuantitySelector(),
                    SizedBox(width: 10),
                  ]),
                  SizedBox(height: 15.0),
                  Text(
                    'Description',
                    style: TextStyle(
                      fontWeight: FontWeight.w900,
                      fontSize: 16.0,
                      decoration: TextDecoration.underline,
                      decorationColor: Colors.yellow[600],
                    ),
                  ),
                  SizedBox(height: 12.0),
                  Expanded(
                    child: SingleChildScrollView(
                      scrollDirection: Axis.vertical,
                      child: Text(
                        'Les pommes de terre sont bénéfiques pour la santé en raison de leur richesse en nutriments essentiels tels que les glucides complexes, les fibres alimentaires, les vitamines (notamment la vitamine C et certaines du groupe B) et les minéraux tels que le potassium et le magnésium. Ces composants contribuent à la fourniture d\'énergie, au maintien d\'une fonction musculaire adéquate, à la régulation de la pression artérielle et au soutien du système immunitaire. De plus, les pommes de terre contiennent des antioxydants qui peuvent aider à neutraliser les radicaux libres dans le corps, contribuant ainsi à la prévention de certaines maladies chroniques. Il est important de souligner que la manière dont les pommes de terre sont préparées, comme la cuisson à la vapeur ou au four plutôt que la friture, peut également influencer leurs bienfaits pour la santé.',
                      ),
                    ),
                  ),
                  SizedBox(height: 12.0),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      ButtonLike(),
                      SizedBox(width: 10),
                      ButtonAddToCart(),
                    ],
                  ),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}
