import 'dart:developer';
import 'package:cashmanagerapp/services/transactionservice.dart';
import 'package:flutter/material.dart';
import 'package:qr_code_scanner/qr_code_scanner.dart';

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
  Barcode? result;
  QRViewController? controller;
  final GlobalKey qrKey = GlobalKey(debugLabel: 'QR');

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
  TextEditingController creditorController = TextEditingController();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Paiement'),
      ),
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
                  style:
                      widget.pos == 0 ? checkButtonStyle : debitCardButtonStyle,
                  child: Text('Carte Bleue'),
                ),
                ElevatedButton(
                  onPressed: () {
                    setState(() {
                      widget.pos = 1;
                    });
                  },
                  style:
                      widget.pos == 1 ? checkButtonStyle : debitCardButtonStyle,
                  child: Text('   Chèque   '),
                ),
              ],
            ),
          ),
          // Content based on the selected option
          widget.pos == 0
              ? Column(
                  children: [
                    Image(
                      image: AssetImage('lib/images/Creditcard.png'),
                    ),
                    SizedBox(height: 10.0),
                    Container(
                      margin:
                          EdgeInsets.only(bottom: 5.0, left: 10.0, right: 10.0),
                      alignment: Alignment.centerLeft,
                      child: Text(
                        'Nom sur la carte',
                        style: TextStyle(
                            fontSize: 15.0, fontWeight: FontWeight.bold),
                      ),
                    ),
                    Container(
                      margin: EdgeInsets.symmetric(horizontal: 10.0),
                      child: TextField(
                        decoration: InputDecoration(
                          hintText: 'John DOE',
                          enabledBorder: OutlineInputBorder(
                            borderSide: BorderSide(
                                color: Color.fromARGB(255, 255, 255, 255),
                                width: 3),
                            borderRadius: BorderRadius.circular(50.0),
                          ),
                          filled: true,
                          fillColor: const Color.fromRGBO(234, 234, 234, 100),
                        ),
                      ),
                    ),
                    Container(
                      margin:
                          EdgeInsets.only(bottom: 5.0, left: 10.0, right: 10.0),
                      alignment: Alignment.centerLeft,
                      child: Text(
                        'Numéro de carte',
                        style: TextStyle(
                            fontSize: 15.0, fontWeight: FontWeight.bold),
                      ),
                    ),
                    Container(
                      margin: EdgeInsets.symmetric(horizontal: 10.0),
                      child: TextField(
                        controller: creditorController,
                        decoration: InputDecoration(
                          hintText: '4275 3156 0372 5493',
                          enabledBorder: OutlineInputBorder(
                            borderSide: BorderSide(
                                color: Color.fromARGB(255, 255, 255, 255),
                                width: 3),
                            borderRadius: BorderRadius.circular(50.0),
                          ),
                          filled: true,
                          fillColor: const Color.fromRGBO(234, 234, 234, 100),
                        ),
                      ),
                    ),
                    Container(
                        margin: EdgeInsets.only(
                            bottom: 5.0, left: 10.0, right: 10.0),
                        alignment: Alignment.centerLeft,
                        child: Row(
                          children: [
                            Expanded(
                              child: Text(
                                'Mois/Année',
                                style: TextStyle(
                                    fontSize: 15.0,
                                    fontWeight: FontWeight.bold),
                              ),
                            ),
                            Expanded(
                              child: Text(
                                'CVV',
                                style: TextStyle(
                                    fontSize: 15.0,
                                    fontWeight: FontWeight.bold),
                              ),
                            ),
                          ],
                        )),
                    Container(
                      margin: EdgeInsets.only(
                          bottom: 20.0, left: 10.0, right: 10.0),
                      child: Row(
                        children: [
                          Expanded(
                            child: TextField(
                              textAlign: TextAlign.center,
                              decoration: InputDecoration(
                                hintText: '1/25',
                                enabledBorder: OutlineInputBorder(
                                  borderSide: BorderSide(
                                      color: Color.fromARGB(255, 255, 255, 255),
                                      width: 3),
                                  borderRadius: BorderRadius.circular(50.0),
                                ),
                                filled: true,
                                fillColor:
                                    const Color.fromRGBO(234, 234, 234, 100),
                              ),
                            ),
                          ),
                          SizedBox(
                              width:
                                  10.0), // Add spacing between the two fields
                          Expanded(
                            child: TextField(
                              textAlign: TextAlign.center,
                              decoration: InputDecoration(
                                hintText: '***',
                                enabledBorder: OutlineInputBorder(
                                  borderSide: BorderSide(
                                      color: Color.fromARGB(255, 255, 255, 255),
                                      width: 3),
                                  borderRadius: BorderRadius.circular(50.0),
                                ),
                                filled: true,
                                fillColor:
                                    const Color.fromRGBO(234, 234, 234, 100),
                              ),
                            ),
                          ),
                        ],
                      ),
                    ),
                    ElevatedButton(
                      onPressed: () {
                        TransactionService().createTransaction(creditorController.text,  widget.pos);
                        Navigator.pop(context);
                      },
                      style: ElevatedButton.styleFrom(
                        padding: EdgeInsets.symmetric(
                            horizontal: 40.0, vertical: 20.0),
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
                      child: Text('Confirmer le paiement'.toUpperCase()),
                    ),
                  ],
                )
              : Column(
                children: [
                  Container(
                    margin: EdgeInsets.only(top: 50.0),
                      child: Image(
                        image: AssetImage('lib/images/check.png'),
                      ),
                  ),
                  SizedBox(height: 10.0),
                  Container(child: _buildQrView(context)), 
                  if (result != null)
                    Text('code: ${result!.code}')
                  else
                    //if code is not scanned, display the text
                    Text('Scanner un chèque'),
                  ElevatedButton(
                      onPressed: () {
                        if (result != null) {
                          TransactionService().createTransaction(result!.code!,  widget.pos);
                        }
                        else{
                          ScaffoldMessenger.of(context).showSnackBar(
                            const SnackBar(content: Text('Scanner un chèque')),
                          );
                        }
                      },
                      style: ElevatedButton.styleFrom(
                        padding: EdgeInsets.symmetric(
                            horizontal: 40.0, vertical: 20.0),
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
                      child: Text('Confirmer le paiement'.toUpperCase()),
                    ),                
                ],
              ),
        ],
      ),
    );
  }

    Widget _buildQrView(BuildContext context) {
      return LayoutBuilder(
        builder: (context, constraints) {
          return Stack(
            children: [
                SizedBox(
                  height: 150,
                  width: 300,
                ),
              Positioned.fill(
                child: QRView(
                  key: qrKey,
                  onQRViewCreated: _onQRViewCreated,
                  overlay: QrScannerOverlayShape(
                    borderColor: Colors.red,
                    borderRadius: 10,
                    borderLength: 30,
                    borderWidth: 10,
                    cutOutSize: 300.0,
                  ),
                  onPermissionSet: (ctrl, p) => _onPermissionSet(context, ctrl, p),
                ),
              ),
            ],
          );
        },
      );
    }

  void _onQRViewCreated(QRViewController controller) {
    setState(() {
      this.controller = controller;
    });
    controller.scannedDataStream.listen((scanData) {
      setState(() {
        result = scanData;
      });
    });
  }

  void _onPermissionSet(BuildContext context, QRViewController ctrl, bool p) {
    log('${DateTime.now().toIso8601String()}_onPermissionSet $p');
    if (!p) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('no Permission')),
      );
    }
  }

  @override
  void dispose() {
    controller?.dispose();
    super.dispose();
  }
}

// ignore: must_be_immutable
class CheckPage extends StatefulWidget {
  var pos = 1;

  CheckPage({required this.pos});

  @override
  State<CheckPage> createState() => _CheckPageState();
}

class _CheckPageState extends State<CheckPage> {
  ElevatedButton buildCheckButton() {
    return ElevatedButton(
      onPressed: () {
        setState(() {
          widget.pos = 1;
        });
      },
      style: checkButtonStyle,
      child: Text('Chèque'),
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
