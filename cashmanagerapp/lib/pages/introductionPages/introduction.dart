import 'package:cashmanagerapp/widgets/app_large_text.dart';
import 'package:cashmanagerapp/widgets/app_text.dart';
import 'package:flutter/material.dart';

class Introduction extends StatefulWidget{
  const Introduction({Key? key}) : super(key: key);

  @override
  _IntroductionState createState() => _IntroductionState();
}

class _IntroductionState extends State<Introduction>{
  List image = [
    'lib/images/intro1.png',
    'lib/images/intro2.png',
    'lib/images/intro3.png',
  ];
  List textLarge=[
    '',
    'Bienvenue sur Cash Manager',
    'A vos courses !'
  ];
  List text=[
    '',
    'L\'application qui accompagne vos courses',
    'Scannez vos produits et ajoutez les à votre panier'
  ];
  @override
  Widget build(BuildContext context){
    return Scaffold(
     body: PageView.builder(
      scrollDirection: Axis.vertical,
      itemCount: 3,
      itemBuilder: (_, index) {
      return Container(
        
        width: double.maxFinite,
        height: double.maxFinite,
        decoration: BoxDecoration(
          color: Color(0xFFFFF500),
          image: DecorationImage(
            image: AssetImage(image[index]),
            //image centered and scaled
            fit: BoxFit.scaleDown,
            alignment: Alignment.center,
          ),
        ),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Column(
                mainAxisAlignment: MainAxisAlignment.center,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  AppLargeText(text: textLarge[index]),
                  AppText(text: text[index]),
                ],
              ),
              Column(
                children: List.generate(3, (indexDots){
                  return Container(
                    width: 8,
                    height: index == indexDots?25:8,
                    decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(8),
                      color: index ==  indexDots? Colors.black : Colors.grey,
                    ),
                  );
                }),
              )
          ],
        ),
      );
     }),
    );
  }
}