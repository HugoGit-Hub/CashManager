import 'package:flutter/material.dart';

class ButtonLike extends StatelessWidget {
  const ButtonLike({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Container(
        decoration: BoxDecoration(
          borderRadius: BorderRadius.all(Radius.circular(50.0)),
        ),
        child: MaterialButton(
          onPressed: () {},
          color: Color(0xFF12B76A),
          child: Row(
            children: [
              Icon(Icons.favorite, color: Colors.white),
            ],
          ),
        ),
      ),
    );
  }
}

class Detail extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Detail page'),
      ),
      body: Column(
        children: [
          Expanded(
            child: Image.asset(
              'lib/images/top-view-raw-potatoes-table.jpg',
              fit: BoxFit.cover,
            ),
          ),
          Expanded(
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
                  Row(
                      mainAxisAlignment: MainAxisAlignment.spaceBetween,
                      children: [
                        Text(
                          '5€/kg',
                          style: TextStyle(
                            fontWeight: FontWeight.bold,
                            fontSize: 14.0,
                            color: Colors.yellow[600],
                          ),
                        ),
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
                            'Les pommes de terre sont bénéfiques pour la santé en raison de leur richesse en nutriments essentiels tels que les glucides complexes, les fibres alimentaires, les vitamines (notamment la vitamine C et certaines du groupe B) et les minéraux tels que le potassium et le magnésium. Ces composants contribuent à la fourniture d\'énergie, au maintien d\'une fonction musculaire adéquate, à la régulation de la pression artérielle et au soutien du système immunitaire. De plus, les pommes de terre contiennent des antioxydants qui peuvent aider à neutraliser les radicaux libres dans le corps, contribuant ainsi à la prévention de certaines maladies chroniques. Il est important de souligner que la manière dont les pommes de terre sont préparées, comme la cuisson à la vapeur ou au four plutôt que la friture, peut également influencer leurs bienfaits pour la santé.')),
                  ),
                  SizedBox(height: 12.0),
                  Row(
                      mainAxisAlignment: MainAxisAlignment.spaceBetween,
                      children: [
                        ButtonLike(),
                        SizedBox(width: 10),
                        ElevatedButton.icon(
                          onPressed: () {},
                          icon: Icon(Icons.add_shopping_cart),
                          label: Text('Ajouter au panier'),
                        ),
                      ]),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}
