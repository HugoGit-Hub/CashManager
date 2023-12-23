import 'package:flutter/material.dart';

class Paiement extends StatefulWidget {
  Paiement({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _PaiementState();
}

class _PaiementState extends State<Paiement> {
  var pos = 0;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: pos == 0 ? DebitCardPage(pos: pos) : CheckPage(pos: pos),
    );
  }
}

// ignore: must_be_immutable
class DebitCardPage extends StatefulWidget {
   var pos = 0;

  DebitCardPage({required this.pos});

  @override
  State<DebitCardPage> createState() => _DebitCardPageState();
}

class _DebitCardPageState extends State<DebitCardPage> {
  ElevatedButton buildDebitCardButton() {
  return ElevatedButton(
    onPressed: () {
      setState(() {
        widget.pos = 0;
      });
    },
    style: debitCardButtonStyle,
    child: Text('Credit Card'),
  );
}
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Column(
        children: [
          Container(
            margin: EdgeInsets.only(top: 40.0, bottom: 20.0),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceAround,
              children: [
                ElevatedButton(
                  onPressed: () {
                    setState(() {
                      widget.pos = 0;
                    });
                  },
                  style: widget.pos == 0 ? checkButtonStyle : debitCardButtonStyle ,
                  child: Text('Credit Card'),
                ),

                ElevatedButton(
                  onPressed: () {
                    setState(() {
                      widget.pos = 1;
                    });
                  },
                  style: widget.pos == 1 ?   checkButtonStyle : debitCardButtonStyle,
                  child: Text('     Check     '),
                ),
              ],
            ),
          ),
          // Content based on the selected option
          widget.pos == 0
    ? Column(
        children: [
          Image(
            image: AssetImage('lib/images/CreditCard.png'),
          ),
          SizedBox(height: 10.0),
          Container(
            margin: EdgeInsets.only(bottom: 5.0, left: 10.0, right: 10.0),
            alignment: Alignment.centerLeft,
            child: Text(
              'Card Holder Name',
              style: TextStyle(fontSize: 15.0, fontWeight: FontWeight.bold),
            ),
          ),
          Container(
            margin: EdgeInsets.symmetric(horizontal: 10.0),
            child: TextField(
              decoration: InputDecoration(
                hintText: 'Ryan THOMAS',
                enabledBorder: OutlineInputBorder(
                  borderSide:
                      BorderSide(color: Color.fromARGB(255, 255, 255, 255), width: 3),
                  borderRadius: BorderRadius.circular(50.0),
                ),
                filled: true,
                fillColor: const Color.fromRGBO(234, 234, 234, 100),
              ),
            ),
          ),
          Container(
            margin: EdgeInsets.only(bottom: 5.0, left: 10.0, right: 10.0),
            alignment: Alignment.centerLeft,
            child: Text(
              'Card Number',
              style: TextStyle(fontSize: 15.0, fontWeight: FontWeight.bold),
            ),
          ),
          Container(
            margin: EdgeInsets.symmetric(horizontal: 10.0),
            child: TextField(
              decoration: InputDecoration(
                hintText: '4275 3156 0372 5493',
                enabledBorder: OutlineInputBorder(
                  borderSide:
                      BorderSide(color: Color.fromARGB(255, 255, 255, 255), width: 3),
                  borderRadius: BorderRadius.circular(50.0),
                ),
                filled: true,
                fillColor: const Color.fromRGBO(234, 234, 234, 100),
                ),
              ),
            ),
          Container(
            margin: EdgeInsets.only(bottom: 5.0, left: 10.0, right: 10.0),
            alignment: Alignment.centerLeft,
            child: Row(
              children: [
                Expanded(
                  child: Text(
                    'Month/Year',
                    style: TextStyle(fontSize: 15.0, fontWeight: FontWeight.bold),
                  ),
                ),
                Expanded(
                  child: Text(
                    'CVV',
                    style: TextStyle(fontSize: 15.0, fontWeight: FontWeight.bold),
                  ),
                ),
              ],
            )
          ),
          Container(
          margin: EdgeInsets.only(bottom: 20.0, left: 10.0, right: 10.0),
          child: Row(
            children: [
              Expanded(
                child: TextField(
                  textAlign: TextAlign.center,
                  decoration: InputDecoration(
                    hintText: '1/25',
                    enabledBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Color.fromARGB(255, 255, 255, 255), width: 3),
                      borderRadius: BorderRadius.circular(50.0),
                    ),
                    filled: true,
                    fillColor: const Color.fromRGBO(234, 234, 234, 100),
                  ),
                ),
              ),
              SizedBox(width: 10.0), // Add spacing between the two fields
              Expanded(
                child: TextField(
                  textAlign: TextAlign.center,
                  decoration: InputDecoration(
                    hintText: '***',
                    enabledBorder: OutlineInputBorder(
                      borderSide: BorderSide(color: Color.fromARGB(255, 255, 255, 255), width: 3),
                      borderRadius: BorderRadius.circular(50.0),
                    ),
                    filled: true,
                    fillColor: const Color.fromRGBO(234, 234, 234, 100),
                  ),
                ),
              ),
            ],
          ),
        ),
          ElevatedButton(
            onPressed: () {
              // Additional action for Debit Card validation
            },
            style: ElevatedButton.styleFrom(
              padding: EdgeInsets.symmetric(horizontal: 40.0, vertical: 20.0),
              textStyle: TextStyle(
                fontWeight: FontWeight.bold,
                fontSize: 20.0,
              ),
              backgroundColor: Colors.yellow,
              foregroundColor: const Color.fromARGB(255, 0, 0, 0),
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(50.0),
              ),
            ),
            child: Text('CONFIRM PAYMENT'),
            
          ),
        ],
      )
    : Container(
        margin: EdgeInsets.only(top: 50.0),
        child: Image(
          image: AssetImage('lib/images/Check.png'),
        ),
      ),
        ],
      ),
    );
  }
}


// ignore: must_be_immutable
class CheckPage extends StatefulWidget {
  var pos = 1;

  CheckPage({required this.pos});

  @override
  State<CheckPage> createState() => _CheckPageState();
}

class _CheckPageState extends State<CheckPage>{
  ElevatedButton buildCheckButton() {
  return ElevatedButton(
    onPressed: () {
      setState(() {
        widget.pos = 1;
      });
    },
    style: checkButtonStyle,
    child: Text('Check'),
  );
}
  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    throw UnimplementedError();
  }

}

final ButtonStyle debitCardButtonStyle = ElevatedButton.styleFrom(
  padding: EdgeInsets.symmetric(horizontal: 40.0, vertical: 20.0),
  textStyle: TextStyle(
    fontWeight: FontWeight.bold,
    fontSize: 20.0,
  ),
  backgroundColor: const Color.fromARGB(255, 255, 255, 255),
  foregroundColor: Colors.black,
  shape: RoundedRectangleBorder(
    borderRadius: BorderRadius.circular(15.0),
  ),
  side: BorderSide(color: Colors.black, width: 2.0),
);

final ButtonStyle checkButtonStyle = ElevatedButton.styleFrom(
  padding: EdgeInsets.symmetric(horizontal: 40.0, vertical: 20.0),
  textStyle: TextStyle(
    fontWeight: FontWeight.bold,
    fontSize: 20.0,
  ),
  backgroundColor: Colors.yellow,
  foregroundColor: Colors.white,
  shape: RoundedRectangleBorder(
    borderRadius: BorderRadius.circular(15.0),
  ),
);
