﻿using Business.Abstract;
using Business.Constants;
using Core.Business;
using Core.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CreditCardManager : ICreditCardService
    {
        ICreditCardDal _creditCardDal;

        public CreditCardManager(ICreditCardDal creditCardDal)
        {
            _creditCardDal = creditCardDal;
        }

        public IResult Add(CreditCard creditCard)
        {
            var creditCardOfCustomer = _creditCardDal.Get(cc => cc.CustomerId == creditCard.CustomerId && cc.CardNumber.Equals(creditCard.CardNumber));
            if (creditCardOfCustomer is null)
            {
                _creditCardDal.Add(creditCard);
                return new SuccessResult(Messages.CreditCardAddedSuccessfully);
            }
            return new SuccessResult(Messages.CreditCardAddedSuccessfully);
        }

        public IResult Delete(CreditCard creditCard)
        {
            _creditCardDal.Delete(creditCard);
            return new SuccessResult(Messages.CreditCardDeletedSuccessfully);
        }

        public IDataResult<List<CreditCard>> GetAll()
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDal.GetAll());
        }

        public IDataResult<List<CreditCard>> GetByCustomerId(int customerId)
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDal.GetAll(cc => cc.CustomerId == customerId).ToList());
        }

        public IResult Update(CreditCard creditCard)
        {
            _creditCardDal.Update(creditCard);
            return new SuccessResult(Messages.CreditCardUpdated);
        }
    }
}