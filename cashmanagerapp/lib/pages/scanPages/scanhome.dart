import 'dart:developer';
import 'dart:io';

import 'package:cashmanagerapp/pages/detailPages/detail.dart';
import 'package:flutter/material.dart';
import 'package:qr_code_scanner/qr_code_scanner.dart';

class ScanHome extends StatefulWidget {
  const ScanHome({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _ScanHomeState();
}

class _ScanHomeState extends State<ScanHome> {
  Barcode? result;
  QRViewController? controller;
  final GlobalKey qrKey = GlobalKey(debugLabel: 'QR');

  // In order to get hot reload to work we need to pause the camera if the platform
  // is android, or resume the camera if the platform is iOS.
  @override
  void reassemble() {
    super.reassemble();
    if (Platform.isAndroid) {
      controller!.pauseCamera();
    }
    controller!.resumeCamera();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Scan'),
      ),
      body: Column(
        children: <Widget>[
          Expanded(flex: 4, child: _buildQrView(context)),
          //redirect to detail page when code is scanned
          Expanded(
            flex: 1,
            child: FittedBox(
              fit: BoxFit.contain,
              child: Column(
                mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                children: <Widget>[
                  //if code is scanned, display the code
                  if (result != null)
                    Text('code: ${result!.code}')
                  else
                    //if code is not scanned, display the text
                    Text('Scanner un code'),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: <Widget>[
                      //if code is scanned, display the button to redirect to detail page
                      if (result != null)
                        ElevatedButton(
                            onPressed: () {
                              Navigator.push(
                                context,
                                MaterialPageRoute(
                                  builder: (context) => Detail(
                                    idArticle: result!.code,
                                    quantity: 1,
                                  ),
                                ),
                              );
                            },
                            child: Text('Aller à la page détail'))
                      else
                        //if code is not scanned, display the button to redirect to category page
                        ElevatedButton(
                            onPressed: () {
                              Navigator.pushNamed(context, '/category');
                            },
                            child: Text('Aller à la page catégories'))
                    ],
                  ),
                ],
              ),
            ),
          )
        ],
      ),
    );
  }

  Widget _buildQrView(BuildContext context) {
    // For this example we check how width or tall the device is and change the scanArea and overlay accordingly.
    var scanArea = (MediaQuery.of(context).size.width < 400 ||
            MediaQuery.of(context).size.height < 400)
        ? 150.0
        : 300.0;
    // To ensure the Scanner view is properly sizes after rotation
    // we need to listen for Flutter SizeChanged notification and update controller
    return QRView(
      key: qrKey,
      onQRViewCreated: _onQRViewCreated,
      overlay: QrScannerOverlayShape(
          borderColor: Colors.red,
          borderRadius: 10,
          borderLength: 30,
          borderWidth: 10,
          cutOutSize: scanArea),
      onPermissionSet: (ctrl, p) => _onPermissionSet(context, ctrl, p),
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
